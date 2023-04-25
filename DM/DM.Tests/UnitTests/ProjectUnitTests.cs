using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.Controllers;
using DM.DAL;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DM.Tests.UnitTests
{
    public class ProjectUnitTests
    {
        #region ProjectGetAllPositiveTesting

        [Fact]
        public async Task ProjectGetAllPositiveTesting()
        {
            var projectRepo = new Mock<IProjectService>();
            var dmContext = new Mock<DmDbContext>();
            var projectController = new ProjectController(dmContext.Object, new CurrentUserService(dmContext.Object, null), projectRepo.Object, null, null);
            var projectListResult = new List<ProjectForReadModel>();
            const string title = "titleProject";
            projectListResult.Add(new ProjectForReadModel() { Title = title });

            projectRepo.Setup(x => x.GetAll())
                .Returns(Task.FromResult(projectListResult));
            var result = await projectController.GetAll();
            var actualResult = result as OkObjectResult;
            var model = (actualResult?.Value as IEnumerable)!.Cast<ProjectModel>().First();

            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(model);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(title, model.Title);
        }

        #endregion
    }
}