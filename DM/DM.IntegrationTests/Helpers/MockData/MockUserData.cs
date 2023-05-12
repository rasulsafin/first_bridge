using System;
using System.Collections.Generic;
using DM.DAL.Entities;
using DM.Domain.DTO;

namespace DM.IntegrationTests.Helpers.MockData
{
    public static class MockUserData
    {
        public static User USER_ENTITY= new()
        {
            Name = "User",
            FathersName = "Current",
            LastName = "Entity",
            Login = "current_11",
            Email = "current11@mail.ru",
            HashedPassword = "current1234",
            Position = "Current 4 Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static AuthenticateRequest AUTH_REQUEST_POSITIVE = new()
        {
            Login = "create@mail.ru",
            Password = "create_password",
        };

        public static AuthenticateRequest AUTH_REQUEST_NEGATIVE = new()
        {
            Login = "qwerrewqqwer",
            Password = "zxcvvcxzzxcv",
        };

        public static UserForCreateDto USER_FOR_CREATE = new()
        {
            Name = "User",
            FathersName = "For",
            LastName = "Create",
            Login = "create1234",
            Email = "create@mail.ru",
            HashedPassword = "create_password",
            Position = "Create Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static UserForUpdateDto USER_FOR_UPDATE = new()
        {
            Name = "User",
            FathersName = "For",
            LastName = "Update",
            Login = "update1234",
            Email = "update@mail.ru",
            HashedPassword = "update_password",
            Position = "Update Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static UserForReadDto USER_FOR_READ_1 = new()
        {
            Name = "User",
            FathersName = "For",
            LastName = "Read",
            Login = "read1234",
            Email = "testread1@mail.ru",
            HashedPassword = "test_read_1_password",
            Position = "Test 1 Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static UserForReadDto USER_FOR_READ_2 = new()
        {
            Name = "User",
            FathersName = "For",
            LastName = "Read",
            Login = "read4321",
            Email = "testread2@mail.ru",
            HashedPassword = "test_read_2_password",
            Position = "Test 2 Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static UserForReadDto USER_FOR_READ_3 = new()
        {
            Name = "User",
            FathersName = "For",
            LastName = "Read",
            Login = "read2341",
            Email = "testread3@mail.ru",
            HashedPassword = "test_read_3_password",
            Position = "Test 3 Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static UserForReadDto USER_FOR_READ_4 = new()
        {
            Name = "User",
            FathersName = "For",
            LastName = "Read",
            Login = "read4132",
            Email = "testread3@mail.ru",
            HashedPassword = "test_read_4_password",
            Position = "Test 4 Position",
            RoleId = 1,//TODO real id,
            OrganizationId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static List<UserForReadDto> LIST_OF_USERS_FOR_READ = new()
        {
            USER_FOR_READ_1, USER_FOR_READ_2, USER_FOR_READ_3, USER_FOR_READ_4
        };
    }
}
