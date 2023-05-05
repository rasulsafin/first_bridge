using AutoMapper;
using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public static class MockUserData
    {
        public static UserForCreateDto USER_FOR_CREATE = new()
        {
            Name = "Test",
            FathersName = "Test",
            LastName = "Test",
            Login = "test1234",
            Email = "test@mail.ru",
            HashedPassword = "test_password",
            Position = "Test Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };
    }
}
