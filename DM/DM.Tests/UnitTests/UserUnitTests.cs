using System;
using System.Threading.Tasks;
using DM.Controllers;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DM.Tests.UnitTests
{
    public class UserUnitTests
    {
        #region Const

        private const string UserName = "Robert",
            LastName = "Reiter",
            FathersName = "J",
            Email = "brogrammer@mail.ru",
            Login = "bromigo",
            Role = "User Admin SuperAdmin",
            Password = "1234",
            Position = "developer",
            Snils = "000";
        
        private const long OrganizationId = 1;

        #endregion

        #region CreateUserReturnsOkPositiveTesting

        [Fact]
        public async Task CreateUserReturnsOkPositiveTesting()
        {
            // preparation
            var userRepo = new Mock<IUserService>();
            var userController = new UsersController(userRepo.Object);

            var userModel = new UserModel()
            { Name = UserName, LastName = LastName, FathersName = FathersName, Birthdate = DateTime.Now, 
                Email = Email, Login = Login, OrganizationId = OrganizationId, Password = Password,
                Roles = Role, Position = Position, Snils = Snils };
            
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
            var userRepo = new Mock<IUserService>();
            var userController = new UsersController(userRepo.Object);

            var userModel = new UserModel()
            { Name = UserName, LastName = LastName, FathersName = FathersName, Birthdate = DateTime.Now, 
                Email = Email, Login = Login, OrganizationId = OrganizationId, Password = Password,
                Roles = "SOMEBODY ONCE TOLD ME", Position = Position, Snils = Snils };
            
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
            var userRepo = new Mock<IUserService>();
            var userController = new UsersController(userRepo.Object);

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
            var userRepo = new Mock<IUserService>();
            var userController = new UsersController(userRepo.Object);

            var result = userController.Authenticate(new AuthenticateRequest()
                { Login = Login, Password = Password });
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        #endregion
    }
    }