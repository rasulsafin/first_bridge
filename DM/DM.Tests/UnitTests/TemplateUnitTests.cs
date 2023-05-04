using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.Controllers;
using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DM.Tests.UnitTests
{
    public class TemplateUnitTests
    {
        //[Fact]
        //public async Task CreatePermissionWithEmptyRequestReturnsBadRequest()
        //{
        //    var templateRepo = new Mock<ITemplateService>();
        //    var permissionController = new TemplateController(null, null, templateRepo.Object, null);

        //    var result = permissionController.AddTemplateToProject(null);

        //    var actualResult = result as BadRequestObjectResult;

        //    Assert.Equal(ErrorList.BadRequest, actualResult?.Value);
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task GetProjectTemplateOfRecordReturnOkAndCorrectObject()
        //{
        //    const string tempName = "Temp";
        //    const int projId = 1;
        //    var templateRepo = new Mock<ITemplateService>();
        //    var templateController = new TemplateController(null, null, templateRepo.Object, null);

        //    var templateResult = new List<TemplateDto>();
        //    templateResult.Add(new TemplateDto() { Name = tempName });
        //    templateResult.Add(new TemplateDto() { ProjectId = projId });

        //    templateRepo.Setup(x => x.GetAllOfProject(1))
        //        .Returns(templateResult);
        //    var result = await templateController.GetProjectTemplateOfRecord(1);

        //    var actualResult = result as OkObjectResult;
        //    var enumerableValue = actualResult?.Value as IEnumerable;

        //    var fieldOfReceivedObject = enumerableValue?.Cast<TemplateDto>().First().Name;
        //    var fieldOfSecondReceivedObject = enumerableValue?.Cast<TemplateDto>().ElementAtOrDefault(1).ProjectId;

        //    Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(tempName, fieldOfReceivedObject);
        //    Assert.Equal(projId, fieldOfSecondReceivedObject);
        //}

        [Fact]
        public async Task GetProjectTemplateOfRecordReturnBadRequestForNonExistingAndCorrectObject()
        {
            var templateRepo = new Mock<ITemplateService>();
            var templateController = new TemplateController(null, templateRepo.Object);

            var result = await templateController.GetProjectTemplateOfRecord(100);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}