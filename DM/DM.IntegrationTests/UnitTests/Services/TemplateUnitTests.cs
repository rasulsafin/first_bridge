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
    public class TemplateUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static TemplateService service;
        private static CurrentUserService currentUserService;
        private static readonly ILogger<TemplateService> logger;

        public TemplateUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;

            currentUserService = new CurrentUserService(UnitOfWork, _mapper);
            currentUserService.SetCurrentUser(1);

            service = new TemplateService(UnitOfWork, _mapper, currentUserService);
        }

        #endregion

        #region Get Testing

        [Fact]
        public async Task GetAllOfProject_Positive()
        {
            var templates = await service.GetAllOfProject(1);
            var result = templates.Any();

            Assert.True(result);
        }

        [Fact]
        public void GetProjectById_Positive()
        {
            var template = service.GetById(MockServiceData.POSITIVE_ID);

            Assert.NotNull(template);
        }

        [Fact]
        public void GetProjectByLargeId_Negative()
        {
            var template = service.GetById(MockServiceData.LARGE_ID);

            Assert.Null(template);
        }

        [Fact]
        public void GetProjectByZeroId_Negative()
        {
            var template = service.GetById(MockServiceData.ZERO_ID);

            Assert.Null(template);
        }

        [Fact]
        public void GetProjectByNegativeId_Negative()
        {
            var template = service.GetById(MockServiceData.NEGATIVE_ID);

            Assert.Null(template);
        }

        #endregion

        #region Create Testing

        [Fact]
        public async Task CreateProjectAndGetById_Positive()
        {
            var templates = await service.GetAllOfProject(1);
            var id_without_added = templates.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockTemplateData.TEMPLATE_FOR_CREATE);

            var template = service.GetById(id_without_added++);

            Assert.NotNull(template);
        }

        #endregion

        #region Update Testing

        [Fact]
        public async Task UpdateExistingProject_Positive()
        {
            var templates = await service.GetAllOfProject(1);
            var id_without_added = templates.LastOrDefault().Id;

            await service.Create(MockTemplateData.TEMPLATE_FOR_CREATE);

            var template = service.GetById(id_without_added++);

            var updated_template = MockTemplateData.TEMPLATE_FOR_UPDATE;
            updated_template.Id = template.Id;

            var result = await service.Update(updated_template);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateNonExistingProject_Negative()
        {
            TemplateForUpdateDto templateForUpdateDto = new()
            {
                Id = MockServiceData.LARGE_ID,
                Name = "Template - 1 (new Name)",
            };

            var result = await service.Update(templateForUpdateDto);

            Assert.False(result);
        }

        #endregion
    }
}