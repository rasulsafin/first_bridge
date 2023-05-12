using System;

using DM.Domain.DTO;

using DM.Common.Enums;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockFieldData
    {
        public static FieldDto FIELD_1 = new()
        {
            Name = "Description",
            IsMandatory = true,
            Data = "This data contains description",
            Type = FieldEnum.Text,
            RecordId = 1,//TODO real id,
            TemplateId = null,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static FieldDto FIELD_2 = new()
        {
            Name = "Executor",
            IsMandatory = false,
            Data = "Sidorov A",
            Type = FieldEnum.Text,
            RecordId = null,//TODO real id,
            TemplateId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static ListFieldDto LIST_FIELD_1 = new()
        {
            Name = "Status_2",
            IsMandatory = false,
            Lists = { LIST_4, LIST_5, LIST_6 },
            Type = FieldEnum.List,
            RecordId = null,//TODO real id,
            TemplateId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static ListFieldDto LIST_FIELD_2 = new()
        {
            Name = "Status_1",
            IsMandatory = false,
            Lists = {LIST_1, LIST_2, LIST_3},
            Type = FieldEnum.Text,
            RecordId = null,//List real id,
            TemplateId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static ListDto LIST_1 = new()
        {
            Data = "Start"
        };

        public static ListDto LIST_2 = new()
        {
            Data = "Ready"
        };

        public static ListDto LIST_3 = new()
        {
            Data = "Closed"
        };

        public static ListDto LIST_4 = new()
        {
            Data = "Preparing"
        };

        public static ListDto LIST_5 = new()
        {
            Data = "In Progress"
        };

        public static ListDto LIST_6 = new()
        {
            Data = "Finished"
        };
    }
}
