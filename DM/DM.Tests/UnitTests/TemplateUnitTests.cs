using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class TemplateUnitTests
    {
        [Fact]
        public async Task CreatePermissionWithEmptyRequestReturnsBadRequest()
        {
            var templateRepo = new Mock<ITemplateService>();
            var permissionController = new TemplateController(templateRepo.Object);

            var result = permissionController.AddTemplateToProject(null);
            
            var actualResult = result as BadRequestObjectResult;
            
            Assert.Equal(ErrorList.BadRequest, actualResult?.Value);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public async Task GetProjectTemplateOfRecordReturnOkAndCorrectObject()
        {
            const string tempName = "Temp";
            const int projId = 1;
            var templateRepo = new Mock<ITemplateService>();
            var templateController = new TemplateController(templateRepo.Object);

            var templateResult = new List<TemplateModel>();
            templateResult.Add(new TemplateModel() {Name = tempName});
            templateResult.Add(new TemplateModel() {ProjectId = projId});

            templateRepo.Setup(x => x.GetTemplatesOfProject(1))
                .Returns(Task.FromResult(templateResult));
            var result = await templateController.GetProjectTemplateOfRecord(1);
            
            var actualResult = result as OkObjectResult;
            var enumerableValue = actualResult?.Value as IEnumerable;
            
            var fieldOfReceivedObject = enumerableValue?.Cast<TemplateModel>().First().Name;
            var fieldOfSecondReceivedObject = enumerableValue?.Cast<TemplateModel>().ElementAtOrDefault(1).ProjectId;
            
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tempName, fieldOfReceivedObject);
            Assert.Equal(projId, fieldOfSecondReceivedObject);
        }
        
        [Fact]
        public async Task GetProjectTemplateOfRecordReturnBadRequestForNonExistingAndCorrectObject()
        {
            var templateRepo = new Mock<ITemplateService>();
            var templateController = new TemplateController(templateRepo.Object);
            
            var result = await templateController.GetProjectTemplateOfRecord(100);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}