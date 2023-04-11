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
    // 'JsonDocument'...
    public class TestDbContext : DmDbContext
    {
        public TestDbContext() : base((new DbContextOptionsBuilder<DmDbContext>()
            .UseInMemoryDatabase(
                Guid.NewGuid().ToString()).Options))
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // попытка решения InvalidOperationException for JsonDocument
            //modelBuilder.Entity<RecordEntity>().Property(p => p.Fields)
            //    .HasConversion(
            //        v => v.ToJsonString(),
            //        v => JsonDocument.Parse(v, new JsonDocumentOptions()));   
        }

        private static string JsonDocumentToString(JsonDocument document)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new Utf8JsonWriter(stream, new JsonWriterOptions {Indented = true});
                document.WriteTo(writer);
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}