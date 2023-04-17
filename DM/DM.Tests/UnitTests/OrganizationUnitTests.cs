using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.Controllers;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DM.Tests.UnitTests
{
    public class OrganizationUnitTests
    {
        #region CreateOrganizationPositiveTesting
        [Fact]
        public async Task CreateOrganizationPositiveTesting()
        {
            var organizationRepo = new Mock<IOrganizationService>();
            var organizationController = new OrganizationController(null, null, organizationRepo.Object, null);

            var organizationModelForCreate = new OrganizationModelForCreate()
            {
                Name = "BRIO", Address = "Kazan", Email = "BRIO@mail.ru",
                Inn = "000", Kpp = "000", Ogrn = "000", Phone = "000"
            };
            
            // execution 
            organizationRepo.Setup(x => x.Create(organizationModelForCreate))
                .Returns(Task.FromResult(true));
            
            var result = await organizationController.Create(organizationModelForCreate);
            
            var actualResult = result as OkObjectResult;

            // examination
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, actualResult?.Value);
        }
        

        #endregion

        #region GetOrganizationPositiveTesting

        [Fact]
        public async Task GetOrganizationPositiveTesting()
        {
            // preparation
            const string organizationName = "BRIO";
            const string organizationName2 = "MRS";
            var organizationRepo = new Mock<IOrganizationService>();
            var organizationController = new OrganizationController(null, null, organizationRepo.Object, null);
            var organizationResult = new List<OrganizationEntity>(); // проверяемый объект
            
            organizationResult.Add(new OrganizationEntity() { Name = organizationName});
            organizationResult.Add(new OrganizationEntity() { Name = organizationName2});

            // execution
            organizationRepo.Setup(x => x.GetAll())
                .Returns(Task.FromResult(organizationResult));
            var result = await organizationController.GetAll();
            
            var actualResult = result as OkObjectResult;
            var enumerableValue = actualResult?.Value as IEnumerable;
            
            var fieldOfReceivedObject = enumerableValue?.Cast<OrganizationEntity>().First().Name;
            var fieldOfSecondReceivedObject = enumerableValue?.Cast<OrganizationEntity>().ElementAtOrDefault(1).Name;

            // examination
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(organizationName, fieldOfReceivedObject);
            Assert.Equal(organizationName2, fieldOfSecondReceivedObject);
        }

        #endregion
    }
}