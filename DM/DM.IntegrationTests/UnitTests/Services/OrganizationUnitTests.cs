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
using System;

namespace DM.IntegrationTests.UnitTests.Services
{
    public class OrganizationUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static OrganizationService service;
        private static CurrentUserService currentUserService;
        private static readonly ILogger<OrganizationService> logger;

        public OrganizationUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;

            currentUserService = new CurrentUserService(UnitOfWork, _mapper);
            currentUserService.SetCurrentUser(1);

            service = new OrganizationService(UnitOfWork, _mapper);
        }

        #endregion

        #region Get Testing

        [Fact]
        public async Task GetAllOrganization_Positive()
        {
            var organizations = await service.GetAll();
            var result = organizations.Any();

            Assert.True(result);
        }

        [Fact]
        public void GetOrganizationById_Positive()
        {
            var organization = service.GetById(MockServiceData.POSITIVE_ID);

            Assert.NotNull(organization);
        }

        [Fact]
        public void GetOrganizationByLargeId_Negative()
        {
            var organization = service.GetById(MockServiceData.LARGE_ID);

            Assert.Null(organization);
        }

        [Fact]
        public void GetOrganizationByZeroId_Negative()
        {
            var organization = service.GetById(MockServiceData.ZERO_ID);

            Assert.Null(organization);
        }

        [Fact]
        public void GetOrganizationByNegativeId_Negative()
        {
            var organization = service.GetById(MockServiceData.NEGATIVE_ID);

            Assert.Null(organization);
        }

        #endregion

        #region Create Testing

        [Fact]
        public async Task CreateOrganizationAndGetById_Positive()
        {
            var organizations = await service.GetAll();
            var id_without_added = organizations.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockOrganizationData.ORGANIZATION_FOR_CREATE);

            var organization = service.GetById(id_without_added++);

            Assert.NotNull(organization);
        }

        #endregion

        #region Update Testing

        [Fact]
        public async Task UpdateExistingOrganization_Positive()
        {
            var organizations = await service.GetAll();
            var id_without_added = organizations.LastOrDefault().Id;

            await service.Create(MockOrganizationData.ORGANIZATION_FOR_CREATE);

            var organization = service.GetById(id_without_added++);

            var updated_organization = MockOrganizationData.ORGANIZATION_FOR_UPDATE;
            updated_organization.Id = organization.Id;

            var result = await service.Update(updated_organization);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateNonExistingOrganization_Negative()
        {
            var updated_organization = MockOrganizationData.ORGANIZATION_FOR_UPDATE;
            updated_organization.Id = MockServiceData.LARGE_ID;

            var result = await service.Update(updated_organization);

            Assert.False(result);
        }

        #endregion

        #region Delete Testing

        [Fact]
        public async Task DeleteExistingOrganization_Positive()
        {
            var users = await service.GetAll();
            var id_without_added = users.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockOrganizationData.ORGANIZATION_FOR_CREATE);

            var res = await service.Delete(id_without_added++);

            Assert.True(res);
        }

        #endregion
    }
}