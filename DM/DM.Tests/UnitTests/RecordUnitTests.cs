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
    public class RecordUnitTests
    {
        #region RecordGetAllPositiveTesting

        [Fact]
        public async Task RecordGetAllPositiveTesting()
        {
            var recordRepo = new Mock<IRecordService>();
            var dmContext = new Mock<DmDbContext>();
            var recordController = new RecordController(recordRepo.Object, dmContext.Object, new CurrentUserService(dmContext.Object));
            var recordModel = new RecordModel() { Id = 1, Name = "Record", ProjectId = 1};
            var recordList = new List<RecordModel>();
            recordList.Add(recordModel);
            
            recordRepo.Setup(x => x.GetAll())
                .Returns(recordList);
            var result = recordController.GetAll();
            var actualResult = result as OkObjectResult;
            var resultModel = (actualResult?.Value as IEnumerable)!.Cast<RecordModel>().First();
            Assert.NotNull(resultModel);
            Assert.IsType<OkObjectResult>(result);
            
            Assert.Equal(recordModel.Id, resultModel.Id);
            Assert.Equal(recordModel.Name, resultModel.Name);
            Assert.Equal(recordModel.ProjectId, resultModel.ProjectId);
        }

        #endregion
        
    }
}