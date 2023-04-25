using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DM.DAL.Entities;
using DM.Domain.Implementations;
using DM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using DM.DAL;
using DM.Domain.Helpers;

namespace DM.Tests
{
    // Нужно для интеграционных тестов без использования моков (тестов типа: "если я введу Х получу ли я Y"), то есть для GET/GetById методов
    // Пока не работает по причине: InvalidOperationException: No suitable constructor was found for entity type
    public class TestDbContext : DmDbContext
    {
        public TestDbContext() : base((new DbContextOptionsBuilder<DmDbContext>()
            .UseInMemoryDatabase(
                Guid.NewGuid().ToString()).Options))
        {
        }
    }
}