using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockOrganizationData
    {
        public static OrganizationForCreateDto ORGANIZATION_FOR_CREATE = new()
        {
            Name = "Document Managment",
            Address = "kazan Lipatova 44",
            Inn = "110022",
            Ogrn = "220011",
            Kpp = "011022",
            Phone = "89688559422",
            Email = "email@mail.ru",

            CreatedAt = DateTime.Now,
        };

        public static OrganizationForUpdateDto ORGANIZATION_FOR_UPDATE = new()
        {
            Name = "Document Managment For Update",
            Address = "kazan Gladilova 44",
            Inn = "222222",
            Ogrn = "333333",
            Kpp = "444444",
            Phone = "89688553243",
            Email = "email321@mail.ru",

            CreatedAt = DateTime.Now,
        };
    }
}
