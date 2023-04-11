using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DM.DAL;
using DM.Domain.Helpers;

namespace DM.Domain.Implementations
{
    public class RecordService : IRecordService
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IMapper _mapper;

        private readonly string _checker = "[]!@#$%^&*+=~`";

        public RecordService(DmDbContext context, IMapper mapper, CurrentUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = userService.CurrentUser;
        }

        public List<RecordModel> GetAll()
        {
            var records = _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.Lists)
                .ToList();

            var rec = records.Select(x => x.Lists.ToArray());

            foreach (var r in records)
            {
                if (_currentUser.Roles != "Admin")
                {
                    var permission = AuthorizationHelper.CheckUsersPermissionsById(_context, _currentUser, PermissionType.Record, r.Id);

                    if (permission == null || !permission.Read)
                    {
                        continue;
                    }
                }
            }

            return _mapper.Map<List<RecordModel>>(records);
        }

        public RecordModel GetById(long recordId)
        {
            var record = _context.Records
                .Include(x => x.Comments)
                .Include(x => x.Fields)
                .Include(x => x.Lists)
                .FirstOrDefault(x => x.Id == recordId);

            if (record == null)
            {
                return null;
            }

            return _mapper.Map<RecordModel>(record);
        }

        public async Task<long> Create(RecordModel recordModel)
        {
            var record = _mapper.Map<RecordEntity>(new RecordModel
            {
                Name = recordModel.Name,
                ProjectId = recordModel.ProjectId,
                Comments = recordModel.Comments,
                Fields = recordModel.Fields,
                ListFields = recordModel.ListFields
            });

            var result = await _context.Records.AddAsync(record);

            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        /// <summary>
        /// update fields attached to a record
        /// </summary>
        public async Task<bool> Update(RecordModel record)
        {
            //foreach (var j in record.RecordTemplate) // Валидация специальных символов
            //{
            //    foreach (var s in _checker)
            //    {
            //        if (j.Value.ToString().Contains(s))
            //        {
            //            return false;
            //        }
            //    }
            //}

            var fieldForUpdate = await _context.Records.FirstOrDefaultAsync(x => x.Name == record.Name);

            if (fieldForUpdate == null)
            {
                return false;
            }

            _context.Records.Attach(fieldForUpdate);

            fieldForUpdate.Name = record.Name;
            fieldForUpdate.ProjectId = record.ProjectId;

            await _context.SaveChangesAsync();

            _context.Entry(fieldForUpdate).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long recordId)
        {
            var result = await _context.Records.FirstOrDefaultAsync(x => x.Id == recordId);
            if (result == null)
            {
                return false;
            }

            _context.Records.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
