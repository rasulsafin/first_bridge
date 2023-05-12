using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Logging;

using Xunit;

using DM.Domain.Services;

using DM.DAL.Repositories;

using DM.IntegrationTests.Helpers;
using DM.IntegrationTests.Helpers.MockData;

using DM.Domain.DTO;

namespace DM.IntegrationTests.UnitTests.Services
{
    public class ProjectUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static ProjectService service;
        private static CurrentUserService currentUserService;
        private static readonly ILogger<ProjectService> logger;

        public ProjectUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;

            currentUserService = new CurrentUserService(UnitOfWork, _mapper);
            currentUserService.SetCurrentUser(1);

            service = new ProjectService(UnitOfWork, _mapper, currentUserService);
        }

        #endregion

        #region Get Testing

        [Fact]
        public async Task GetAllProject_Positive()
        {
            var projects = await service.GetAll();
            var result = projects.Any();

            Assert.True(result);
        }

        [Fact]
        public void GetProjectById_Positive()
        {
            var project = service.GetById(MockServiceData.POSITIVE_ID);

            Assert.NotNull(project);
        }

        [Fact]
        public void GetProjectByLargeId_Negative()
        {
            var project = service.GetById(MockServiceData.LARGE_ID);

            Assert.Null(project);
        }

        [Fact]
        public void GetProjectByZeroId_Negative()
        {
            var project = service.GetById(MockServiceData.ZERO_ID);

            Assert.Null(project);
        }

        [Fact]
        public void GetProjectByNegativeId_Negative()
        {
            var project = service.GetById(MockServiceData.NEGATIVE_ID);

            Assert.Null(project);
        }

        #endregion

        #region Create Testing

        [Fact]
        public async Task CreateProjectAndGetById_Positive()
        {
            var projects = await service.GetAll();
            var id_without_added = projects.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockProjectData.PROJECT_FOR_READ);

            var project = service.GetById(id_without_added++);

            Assert.NotNull(project);
        }

        #endregion

        #region Update Testing

        [Fact]
        public async Task UpdateExistingProject_Positive()
        {
            var projects = await service.GetAll();
            var id_without_added = projects.LastOrDefault().Id;

            await service.Create(MockProjectData.PROJECT_FOR_READ);

            var project = service.GetById(id_without_added++);

            ProjectForUpdateDto projectForUpdateDto = new()
            {
                Id = project.Id,
                Title = "Project - 1 (new Title)",
                IsInArchive = true,
            };

            var result = await service.Update(projectForUpdateDto);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateNonExistingProject_Negative()
        {
            ProjectForUpdateDto projectForUpdateDto = new()
            {
                Id = MockServiceData.LARGE_ID,
                Title = "Project - 1 (new Title)",
                IsInArchive = true,
            };

            var result = await service.Update(projectForUpdateDto);

            Assert.False(result);
        }

        #endregion

        #region Delete Testing

        [Fact]
        public async Task ArchiveExistingProject_Positive()
        {
            var users = await service.GetAll();
            var id_without_added = users.LastOrDefault().Id;

            //TODO added role&organization
            await service.Create(MockProjectData.PROJECT_FOR_READ);

            var res = await service.Archive(id_without_added++);

            Assert.True(res);
        }

        #endregion
    }
}