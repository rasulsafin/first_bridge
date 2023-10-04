using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.DAL.Interfaces;

using DM.Common.Enums;
using DM.Common.Helpers;
using DM.Domain.Infrastructure.Exceptions;
using System.Linq;
using Item = DM.DAL.Entities.Item;
using offline_module.Domain.Interfaces;

namespace DM.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly UserDto _currentUser;
        private IUnitOfWork Context { get; set; }

        private readonly IMapper _mapper;

        private readonly IMinIOService _remoteStorageService;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService, IMinIOService remoteStorageService)
        {
            Context = unitOfWork;
            _currentUser = userService.CurrentUser;
            _mapper = mapper;
            _remoteStorageService = remoteStorageService;
        }

        public async Task<IEnumerable<ItemDto>> GetAll(long projectId)
        {
            var items = await Context.Items.GetAllByProject(projectId);
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public ItemDto GetById(long itemId)
        {
            if (itemId < 1) return null;

            var item = Context.Items.GetById(itemId);
            return _mapper.Map<ItemDto>(item);
        }

        public ItemDto Find(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;

            var item = Context.Items.Find(fileName);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<long> Create(ItemDto itemDto, IFormFile file)
        {
            try
            {
                var fileExtension = Path.GetExtension(file.FileName);

                if (FileHelper.ValidateFileExtension(fileExtension))
                {
                    var fileNameWithoutExtension = FileHelper.DeleteFileExtention(file.FileName); // Folder Name
                    var pathSaveFile = FileHelper.PathServerStorage + fileNameWithoutExtension;

                    if (!Directory.Exists(pathSaveFile)) // Check the directory exists
                        Directory.CreateDirectory(pathSaveFile);

                    string version = FileHelper.GenerateFileVersion(pathSaveFile);
                    string pathForCreate = FileHelper.CreateFilePath(fileNameWithoutExtension, version, fileExtension);

                    using (var fstream = new FileInfo(pathForCreate).Create()) // Create instance to put an Object
                    {
                        var c = fstream;
                        await file.CopyToAsync(fstream); // Put an Object
                    }

                    itemDto.Name = fileNameWithoutExtension + "_v" + version + fileExtension;
                    itemDto.RelativePath = pathForCreate;

                    var item = _mapper.Map<Item>(itemDto);

                    await Context.Items.Create(item);
                    await Context.SaveAsync();

                    return item.Id;
                }
                else
                {
                    throw new Exception("Incorrect file extention");
                }
            }
            catch (Exception)
            {
                throw new Exception("Incorrect file extention");
            }
        }

        public async Task<long> LinkItem(long projectId, ItemDto itemDto)
        {
            try
            {
                var project = Context.Projects.GetById(projectId) ?? throw new Exception("Not Found");
                var projectItems = await Context.Items.GetAllByProject(projectId);

                var itemFromDb = projectItems.FirstOrDefault(
                    x => x.Id == itemDto.Id || x.RelativePath == itemDto.RelativePath);
                var isNew = itemFromDb == null;

                var item = itemFromDb;

                if (isNew)
                {
                    itemFromDb = _mapper.Map<Item>(itemDto);
                }

                if (isNew || itemFromDb.Project.Id != projectId)
                {
                    itemFromDb.Project = project;

                    if (isNew)
                        await Context.Items.Create(itemFromDb);
                    else
                        Context.Items.Update(itemFromDb);

                    await Context.SaveAsync();
                }

                return itemFromDb.Id;
            }
            catch (Exception ex)
            {
                throw new DocumentManagementException(ex.Message, ex.StackTrace);
            }
        }


        public async Task<bool> Delete(string fileName)
        {
            try
            {
                var result = false;

                var item = Context.Items.Find(fileName);
                if (item == null) return result;

                FileInfo fileInf = new(item.RelativePath);
                if (fileInf.Exists)
                {
                    fileInf.Delete();

                    result = Context.Items.Delete(item.Id);
                    await Context.SaveAsync();
                }

                return result;
            }
            catch (Exception)
            {
                throw new Exception("Incorrect file extention");
            }
        }

        public async Task<int> UploadItems(int userId, IEnumerable<int> itemIds)
        {
            var resUserId = await _remoteStorageService.UploadItems(userId, itemIds);
            return resUserId;
        }

        public async Task<int> DownloadItems(int userId, IEnumerable<int> itemIds)
        {
            var resUserIdresUserId = await _remoteStorageService.DownloadItems(userId, itemIds);
            return resUserIdresUserId;
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Item);

                return action switch
                {
                    ActionEnum.Read => access.Read,
                    ActionEnum.Create => access.Create,
                    ActionEnum.Delete => access.Delete,
                    ActionEnum.Update => access.Update,
                    _ => false,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
