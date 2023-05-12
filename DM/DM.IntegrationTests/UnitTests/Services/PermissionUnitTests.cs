using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Logging;

using Xunit;

using DM.Domain.Services;

using DM.DAL.Repositories;

using DM.IntegrationTests.Helpers;
using DM.IntegrationTests.Helpers.MockData;

using DM.Domain.DTO;
using DM.DAL.Entities;

namespace DM.IntegrationTests.UnitTests.Services
{
    public class PermissionUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static PermissionService service;
        private static CurrentUserService currentUserService;
        private static readonly ILogger<ProjectService> logger;

        public PermissionUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;

            currentUserService = new CurrentUserService(UnitOfWork, _mapper);
            currentUserService.SetCurrentUser(1);

            service = new PermissionService(UnitOfWork, _mapper);
        }

        #endregion

        #region Get Testing

        [Fact]
        public async Task GetPermissionsByRoleId_Positive()
        {
            var permissions = await service.GetAllByRole(MockServiceData.POSITIVE_ID);
            var result = permissions.Any();

            Assert.True(result);
        }

        [Fact]
        public async Task GetPermissionsByRoleLargeId_Negative()
        {
            var permissions = await service.GetAllByRole(MockServiceData.LARGE_ID);
            var result = permissions.Any();

            Assert.False(result);
        }

        [Fact]
        public async Task GetPermissionsByRoleZeroId_Negative()
        {
            var permissions = await service.GetAllByRole(MockServiceData.ZERO_ID);

            Assert.Null(permissions);
        }

        [Fact]
        public async Task GetPermissionsByRoleNegativeId_Negative()
        {
            var permissions = await service.GetAllByRole(MockServiceData.NEGATIVE_ID);

            Assert.Null(permissions);
        }

        #endregion

        #region Update Testing

        [Fact]
        public async Task UpdateNonExistingProject_Negative()
        {
            PermissionDto projectForUpdateDto = new()
            {
                Id = MockServiceData.LARGE_ID,
                Create = true,
                Delete = false,
            };

            var result = await service.UpdatePermissionOnRole(projectForUpdateDto);

            Assert.False(result);
        }

        #endregion
    }
}