using DM.Domain.DTO;
using System;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockItemData
    {
        public static ItemDto ITEM_1 = new()
        {
            Name = "dm_1.png",
            RelativePath = "indefinite",//TODO real path,
            ProjectId = 1,//TODO real id,

            CreatedAt = DateTime.Now,
        };

        public static ItemDto ITEM_2 = new()
        {
            Name = "dm_2.docx",
            RelativePath = "indefinite",//TODO real path,
            ProjectId = 1,//TODO real id,

            CreatedAt = DateTime.Now,
        };

        public static ItemDto ITEM_3 = new()
        {
            Name = "dm_3.docx",
            RelativePath = "indefinite",//TODO real path,
            ProjectId = 1,//TODO real id,

            CreatedAt = DateTime.Now,
        };

        public static ItemDto ITEM_4 = new()
        {
            Name = "dm_4.xlsx",
            RelativePath = "indefinite",//TODO real path,
            ProjectId = 1,//TODO real id,

            CreatedAt = DateTime.Now,
        };
    }
}
