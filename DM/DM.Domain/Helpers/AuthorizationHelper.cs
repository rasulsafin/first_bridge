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
                                                                && x.RoleId == currentUser.RoleId).Read;

            return permission;
        }

        public static bool CheckUserPermissionsForCreate(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId).Create;

            return permission;
        }

        public static bool CheckUserPermissionsForUpdate(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId).Update;

            return permission;
        }

        public static bool CheckUserPermissionsForDelete(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId).Delete;

            return permission;
        }

        public static PermissionEntity CheckUserPermissionsByFileName(DmDbContext context, UserEntity currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            return permission;
        }
    }
}
