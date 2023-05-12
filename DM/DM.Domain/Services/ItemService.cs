using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

using DM.Common.Enums;
using DM.Common.Helpers;

namespace DM.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly UserDto _currentUser;
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService userService)
        {
            Context = unitOfWork;
            _currentUser = userService.CurrentUser;
            _mapper = mapper;
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
