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

                (int)x.Type == (int)permissionType && x.RoleId == currentUser.RoleId);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsForCreate(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.RoleId == currentUser.RoleId && x.Create == true);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsForUpdate(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.RoleId == currentUser.RoleId && x.Update == true);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsForDelete(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.RoleId == currentUser.RoleId && x.Delete == true);

            return permission;
        }

        public static PermissionEntity CheckUsersPermissionsByFileName(DmDbContext context, UserEntity currentUser, PermissionType permissionType, long id)
        {
            var permission = context.Permissions.FirstOrDefault(x =>

                (int)x.Type == (int)permissionType && x.RoleId == currentUser.RoleId);

            return permission;
        }
    }
}
