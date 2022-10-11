using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.DAL;

namespace DM.Domain.Implementations
{
    public class PermissionService : IPermissionService
    {
        private readonly DmDbContext _context;

        public PermissionService(DmDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPermissionToUser(PermissionModel permissionModel)
        {
            var userChecker = await _context.Users.FirstOrDefaultAsync(x => x.Id == permissionModel.UserId);
            if (userChecker == null)
            {
                return false;
            }

            var fieldForUpdate = await _context.Permissions
                .Where(q => q.UserId == permissionModel.UserId
                                                                  && q.ObjectId == permissionModel.ObjectId &&
                                                                  q.Type == permissionModel.Type).FirstOrDefaultAsync();
            if (fieldForUpdate != null) // обновляем запись, в случае если она существует
            {
                _context.Permissions.Attach(fieldForUpdate);

                fieldForUpdate.Create = permissionModel.Create;
                fieldForUpdate.Read = permissionModel.Read;
                fieldForUpdate.Update = permissionModel.Update;
                fieldForUpdate.Delete = permissionModel.Delete;

                await _context.SaveChangesAsync();

                _context.Entry(fieldForUpdate).State = EntityState.Detached;
            }
            
            // иначе добавляем новую
            _context.Permissions
                .Add(new PermissionEntity()
                {
                    UserId = permissionModel.UserId,
                    ObjectId = permissionModel.ObjectId,
                    Type = permissionModel.Type,
                    Create = permissionModel.Create,
                    Read = permissionModel.Read,
                    Update = permissionModel.Update,
                    Delete = permissionModel.Delete
                });

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PermissionEntity>> GetAllPermissionsOfUser(long userId)
        {
            var result = await _context.Permissions.Where(x => x.UserId == userId).ToListAsync();

            return result;
        }

        public async Task<bool> RemovePermissionFromUser(PermissionModel permissionModel)
        {
            var result = await _context.Permissions
                .Where(x => x.Type == permissionModel.Type && x.UserId == permissionModel.UserId
                && x.ObjectId == permissionModel.ObjectId)
                .FirstOrDefaultAsync();

            _context.Permissions.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
