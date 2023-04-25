using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.Controllers;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DM.DAL.Enums;
using System;

namespace DM.Tests.UnitTests
{
    public class PermissionUnitTests
    {

        private readonly List<PermissionModel> permissions = new List<PermissionModel>
        {
            new PermissionModel { Id=1, RoleId = 1, Type = PermissionType.Project, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionModel { Id=2, RoleId = 1, Type = PermissionType.Role, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionModel { Id=3, RoleId = 1, Type = PermissionType.Organization, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionModel { Id=4, RoleId = 1, Type = PermissionType.Template, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionModel { Id=5, RoleId = 1, Type = PermissionType.Record, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionModel { Id=6, RoleId = 1, Type = PermissionType.Item, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionModel { Id=7, RoleId = 1, Type = PermissionType.User, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
        };

        #region CreatePermissionReturnsOkPositiveTesting

        [Fact]
        public async Task CreatePermissionReturnsOkPositiveTesting()
        {
            var permissionRepo = new Mock<IPermissionService>();
            var permissionController = new PermissionController(null, null, permissionRepo.Object, null);
            var permissionListResult = new List<PermissionEntity>();
            var permissionForResult = new PermissionEntity()
            {
                Role = new RoleEntity() { Name = "Name" },
                Create = true,
                Delete = true,
                Read = true,
                Update = true,
                Type = PermissionType.Item,
                RoleId = 1
            };
            permissionListResult.Add(permissionForResult);

            permissionRepo.Setup(x => x.GetAllByRole(1))
                .Returns(Task.FromResult(permissionListResult));
            var result = await permissionController.GetAllByRole(1);

            var actualResult = result as OkObjectResult;
            var model = (actualResult?.Value as IEnumerable)!.Cast<PermissionEntity>().First();
            Assert.NotNull(model);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(permissionForResult.Create, model.Create);
            Assert.Equal(permissionForResult.Delete, model.Delete);
            Assert.Equal(permissionForResult.Read, model.Read);
            Assert.Equal(permissionForResult.Update, model.Update);
            Assert.Equal(permissionForResult.RoleId, model.RoleId);
        }

        #endregion

        #region GetAllPermissionsReturnsNotFound

        [Fact]
        public async Task GetAllPermissionsReturnsNotFound()
        {
            var permissionRepo = new Mock<IPermissionService>();
            var permissionController = new PermissionController(null, null, permissionRepo.Object, null);

            var result = await permissionController.GetAllByRole(1);
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        //#region AddPermissionsToUserReturnsBadRequestWithNotExistingUser

        //[Fact]
        //public async Task AddPermissionsToUserReturnsBadRequestWithNotExistingUser()
        //{
        //    var permissionRepo = new Mock<IPermissionService>();
        //    var permissionController = new PermissionController(permissionRepo.Object);

        //    var model = new PermissionModel() { RoleId = 1, Create = true };

        //    var result = await permissionController.CreatePermissionToRole(model);
        //    Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal(ErrorList.NotFoundUser,  result.GetPropertyValue("Value"));
        //}

        //#endregion

        //#region AddPermissionsToUserReturnsBadRequestWithWrongRequest

        //[Fact]
        //public async Task AddPermissionsToUserReturnsBadRequestWithWrongRequest()
        //{
        //    var permissionRepo = new Mock<IPermissionService>();
        //    var permissionController = new PermissionController(permissionRepo.Object);

        //    var result = await permissionController.CreatePermissionToRole(null);
        //    Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal(ErrorList.BadRequest,  result.GetPropertyValue("Value"));
        //}

        //#endregion
    }
}