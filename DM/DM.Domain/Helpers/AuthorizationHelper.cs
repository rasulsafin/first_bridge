using System.Linq;

using DM.Domain.Models;

using DM.DAL.Enums;
using DM.DAL;

namespace DM.Domain.Helpers
{
    public static class AuthorizationHelper
    {
        public static bool CheckUserPermissionsForRead(DmDbContext context, UserModel currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Read;
        }

        public static bool CheckUserPermissionsForCreate(DmDbContext context, UserModel currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Create;
        }

        public static bool CheckUserPermissionsForUpdate(DmDbContext context, UserModel currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Update;
        }

        public static bool CheckUserPermissionsForDelete(DmDbContext context, UserModel currentUser, PermissionType permissionType)
        {
            var permission = context.Permissions.FirstOrDefault(x => (int)x.Type == (int)permissionType
                                                                && x.RoleId == currentUser.RoleId);

            if (permission == null) { return false; }

            return permission.Delete;
        }
    }
}
