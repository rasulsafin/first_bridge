using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Xunit;

using DM.Domain.Services;

using DM.DAL.Repositories;

using DM.IntegrationTests.Helpers;
using DM.IntegrationTests.Helpers.MockData;

using DM.Domain.DTO;

namespace DM.IntegrationTests.UnitTests.Services
{
    public class UserUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static UserService service;

        private static readonly IConfiguration conf;
        private static readonly ILogger<UserService> logger;

        public UserUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;
            service = new UserService(UnitOfWork, conf, _mapper, logger);
        }

        #endregion

        #region Authorization testing


        #endregion

        #region Get Testing

        [Fact]
        public async Task GetAllUsers_Positive()
        {
            var users = await service.GetAll();
            var result = users.Any();

            Assert.True(result);
        }

        [Fact]
        public void GetUserById_Positive()
        {
            var user = service.GetById(MockServiceData.POSITIVE_ID);

            Assert.NotNull(user);
        }

        [Fact]
        public void GetUserByLargeId_Negative()
        {
            var user = service.GetById(MockServiceData.LARGE_ID);

            Assert.Null(user);
        }

        [Fact]
        public void GetUserByZeroId_Negative()
        {
            var user = service.GetById(MockServiceData.ZERO_ID);

            Assert.Null(user);
        }

        [Fact]
        public void GetUserByNegativeId_Negative()
        {
            var user = service.GetById(MockServiceData.NEGATIVE_ID);

            Assert.Null(user);
        }

        #endregion

        #region Create Testing

        [Fact]
        public async Task CreateUserAndGetById_Positive()
        {
            var users = await service.GetAll();
            var id_without_added = users.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockUserData.USER_FOR_CREATE);

            var user = service.GetById(id_without_added++);

            Assert.NotNull(user);
        }

        #endregion

        #region Update Testing

        [Fact]
        public async Task UpdateExistingUser_Positive()
        {
            var users = await service.GetAll();
            var id_without_added = users.LastOrDefault().Id;

            await service.Create(MockUserData.USER_FOR_CREATE);

            var user = service.GetById(id_without_added++);

            UserForUpdateDto userForUpdateDto = new()
            {
                Id = user.Id,
                Name = "New Name",
                FathersName = "New FathersName",
                LastName = "New LastName",
            };

            var result = await service.Update(userForUpdateDto);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateNonExistingUser_Negative()
        {
            UserForUpdateDto userForUpdateDto = new()
            {
                Id = MockServiceData.LARGE_ID,
                Name = "New Name",
                FathersName = "New FathersName",
                LastName = "New LastName",
            };

            var result = await service.Update(userForUpdateDto);

            Assert.False(result);
        }

        #endregion

        #region Delete Testing

        [Fact]
        public async Task DeleteExistingUser_Positive()
        {
            var users = await service.GetAll();
            var id_without_added = users.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockUserData.USER_FOR_CREATE);

            var res = await service.Delete(id_without_added++);

            Assert.True(res);
        }

        #endregion
    }
}