using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Moq;
using Xunit;

using DM.Controllers;

using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Domain.Services;

using DM.DAL;

using DM.Tests.Helpers;
using System;

namespace DM.Tests.UnitTests
{
    public class UserUnitTests
    {
        #region Const
        private readonly UserDto user = new()
        {
            Id = 1,
            Name = "Robert",
            LastName = "Reiter",
            FathersName = "J",
            Email = "brogrammer@mail.ru",
            Login = "bromigo",
            HashedPassword = "1234",
            Position = "developer",
            RoleId = 1,
            OrganizationId = 1,
        };

        #endregion

        #region CreateUserReturnsOkPositiveTesting

        [Fact]
        public async Task CreateUserReturnsOkPositiveTesting()
        {
            // preparation
            var dmContext = new Mock<DmDbContext>();
            var userRepo = new Mock<IUserService>();
            var userProjectRepo = new Mock<IUserProjectService>();
            var userController = new UserController(dmContext.Object, null, userRepo.Object, userProjectRepo.Object);

            var userModel = new UserForCreateDto()
            {
                Name = user.Name,
                LastName = user.LastName,
                FathersName = user.FathersName,
                Email = user.Email,
                Login = user.Name,
                OrganizationId = user.OrganizationId,
                HashedPassword = user.HashedPassword,
                RoleId = user.RoleId,
                Position = user.Position
            };

            // execution
            userRepo.Setup(x => x.Create(userModel))
                .Returns(Task.FromResult(true));

            var result = await userController.Create(userModel);
            var actualResult = result as OkObjectResult;

            // examination
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, actualResult?.Value);
        }

        #endregion

        #region CreateUserReturnsBadRequestByNotExistingRoleTest

        [Fact]
        public async Task CreateUserReturnsBadRequestByNotExistingRoleTest()
        {
            // preparation
            var dmContext = new Mock<DmDbContext>();
            var userRepo = new Mock<IUserService>();
            var userProjectRepo = new Mock<IUserProjectService>();
            var userController = new UserController(dmContext.Object, null, userRepo.Object, userProjectRepo.Object);

            var userModel = new UserForCreateDto()
            {
                Name = user.Name,
                LastName = user.LastName,
                FathersName = user.FathersName,
                Email = user.Email,
                Login = user.Name,
                OrganizationId = user.OrganizationId,
                HashedPassword = user.HashedPassword,
                RoleId = 10,
                Position = user.Position
            };

            // execution
            var result = await userController.Create(userModel);

            var actualResult = result as BadRequestObjectResult;

            // examination
            Assert.Equal("The Role does not exist", actualResult?.Value);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region CreateUserReturnsBadRequestByNullTest

        [Fact]
        public async Task CreateUserReturnsBadRequestByNullTest()
        {
            // preparation
            var dmContext = new Mock<DmDbContext>();
            var userRepo = new Mock<IUserService>();
            var userProjectRepo = new Mock<IUserProjectService>();
            var userController = new UserController(null, null, null, null);

            // execution
            var result = await userController.Create(null);

            var actualResult = result as BadRequestObjectResult;

            // examination
            Assert.Equal(ErrorList.BadRequest, actualResult?.Value);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region AuthenticateRequestReturnsBadRequestForEmptyContext

        [Fact]
        public void AuthenticateRequestReturnsBadRequestForEmptyContext()
        {
            var userController = new UserController(null, null, null, null);

            var result = userController.Authenticate(new AuthenticateRequest()
            { Login = user.Login, Password = user.HashedPassword });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        #endregion
    }
}