using System;

using Microsoft.EntityFrameworkCore;

using DM.DAL;
using DM.DAL.Repositories;

namespace DM.IntegrationTests.Helpers
{
    // Нужно для интеграционных тестов без использования моков (тестов типа: "если я введу Х получу ли я Y"), то есть для GET/GetById методов
    public class TestDbContext : IDisposable
    {
        public DmDbContext Context { get; private set; }
        public EFUnitOfWork UnitOfWork { get; private set; }

        public TestDbContext()
        {
            var options = new DbContextOptionsBuilder<DmDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                  .Options;

            Context = new DmDbContext(options);
            UnitOfWork = new EFUnitOfWork(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}