using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.Controllers;
using DM.DAL;
using DM.DAL.Entities;
using DM.Domain.Services;
using DM.Domain.Interfaces;
using DM.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DM.Tests.UnitTests
{
    public class OrganizationUnitTests
    {
        #region Const
        private readonly UserDto user = new()
        {
            Id = 1,
            Name = "Robert",
            LastName = "Reiter",
            FathersName = "J",
            Email = "brogrammer@mail.ru",
            Login = "bromigo",
            HashedPassword = "1234",
            Position = "developer",
            RoleId = 1,
            OrganizationId = 1,
        };

        #endregion

        #region CreateOrganizationPositiveTesting
        [Fact]
        public async Task CreateOrganizationPositiveTesting()
        {
            var dmContext = new Mock<DmDbContext>();
            var organizationRepo = new Mock<IOrganizationService>();
            var organizationController = new OrganizationController(null, organizationRepo.Object);

            var organizationModelForCreate = new OrganizationForCreateDto()
            {
                Name = "BRIO",
                Address = "Kazan",
                Email = "BRIO@mail.ru",
                Inn = "000",
                Kpp = "000",
                Ogrn = "000",
                Phone = "000"
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
            var organizationController = new OrganizationController(null, organizationRepo.Object);
            var organizationResult = new List<OrganizationDto>(); // проверяемый объект

            organizationResult.Add(new OrganizationForCreateDto() { Name = organizationName });
            organizationResult.Add(new OrganizationForCreateDto() { Name = organizationName2 });

            // execution
            organizationRepo.Setup(x => x.GetAll());
            var result = await organizationController.GetAll();

            var actualResult = result as OkObjectResult;
            var enumerableValue = actualResult?.Value as IEnumerable;

            var fieldOfReceivedObject = enumerableValue?.Cast<Organization>().First().Name;
            var fieldOfSecondReceivedObject = enumerableValue?.Cast<Organization>().ElementAtOrDefault(1).Name;

            // examination
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(organizationName, fieldOfReceivedObject);
            Assert.Equal(organizationName2, fieldOfSecondReceivedObject);
        }

        #endregion
    }
}