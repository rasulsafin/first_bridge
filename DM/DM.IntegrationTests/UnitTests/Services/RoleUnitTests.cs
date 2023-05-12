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
    public class RoleUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static RoleService service;
        private static CurrentUserService currentUserService;
        private static readonly IConfiguration conf;
        private static readonly ILogger<RoleService> logger;

        public RoleUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;

            currentUserService = new CurrentUserService(UnitOfWork, _mapper);
            currentUserService.SetCurrentUser(1);

            service = new RoleService(UnitOfWork, _mapper);
        }

        #endregion

        #region Get Testing

        [Fact]
        public async Task GetAllRoles_Positive()
        {
            var roles = await service.GetAll();
            var result = roles.Any();

            Assert.True(result);
        }

        [Fact]
        public void GetRoleById_Positive()
        {
            var role = service.GetById(MockServiceData.POSITIVE_ID);

            Assert.NotNull(role);
        }

        [Fact]
        public void GetRoleByLargeId_Negative()
        {
            var role = service.GetById(MockServiceData.LARGE_ID);

            Assert.Null(role);
        }

        [Fact]
        public void GetRoleByZeroId_Negative()
        {
            var role = service.GetById(MockServiceData.ZERO_ID);

            Assert.Null(role);
        }

        [Fact]
        public void GetRoleByNegativeId_Negative()
        {
            var role = service.GetById(MockServiceData.NEGATIVE_ID);

            Assert.Null(role);
        }

        #endregion

        #region Create Testing

        [Fact]
        public async Task CreateRoleAndGetById_Positive()
        {
            var roles = await service.GetAll();
            var id_without_added = roles.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockRoleData.ROLE_FOR_CREATE);

            var role = service.GetById(id_without_added++);

            Assert.NotNull(role);
        }

        #endregion

        #region Update Testing

        [Fact]
        public async Task UpdateExistingRole_Positive()
        {
            var roles = await service.GetAll();
            var id_without_added = roles.LastOrDefault().Id;

            await service.Create(MockRoleData.ROLE_FOR_CREATE);

            var role = service.GetById(id_without_added++);

            RoleForUpdateDto roleForUpdateDto = new()
            {
                Id = role.Id,
                Name = "Edited Role",
                Description = "Description about Edited Role",
            };

            var result = await service.Update(roleForUpdateDto);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateNonExistingRole_Negative()
        {
            RoleForUpdateDto roleForUpdateDto = new()
            {
                Id = MockServiceData.LARGE_ID,
                Name = "Edited Role",
                Description = "Description about Edited Role",
            };

            var result = await service.Update(roleForUpdateDto);

            Assert.False(result);
        }

        #endregion

        #region Delete Testing

        [Fact]
        public async Task DeleteExistingRole_Positive()
        {
            var roles = await service.GetAll();
            var id_without_added = roles.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockRoleData.ROLE_FOR_CREATE);

            var res = await service.Delete(id_without_added++);

            Assert.True(res);
        }

        #endregion
    }
}
