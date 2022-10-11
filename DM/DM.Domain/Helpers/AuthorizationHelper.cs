using DM.DAL.Entities;
using System.Linq;
using DM.DAL;

namespace DM.Domain.Helpers
{
    public static class AuthorizationHelper
    {
        public static PermissionEntity CheckUsersPermissionsById(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.UserId == currentUser.Id && x.ObjectId == id);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsForCreate(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.UserId == currentUser.Id && x.Create == true);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsForUpdate(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.UserId == currentUser.Id && x.ObjectId == id && x.Update == true);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsForDelete(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.UserId == currentUser.Id && x.ObjectId == id && x.Delete == true);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsByFileName(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.UserId == currentUser.Id && x.ObjectId == id);

            return permission;
        }
    }
}
