using System.Linq;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Helpers
{
    public static class AuthorizationHelper
    {
        public static bool CheckUserPermissionsById(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Read;
        }

        public static bool CheckUserPermissionsForCreate(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Create;
        }

        public static bool CheckUserPermissionsForUpdate(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Update;
        }

        public static bool CheckUserPermissionsForDelete(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Delete;
        }

        public static PermissionEntity CheckUserPermissionsByFileName(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            return permission;
        }
    }
}
