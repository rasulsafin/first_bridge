using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockCommentData
    {
        public static CommentForReadDto COMMENT_FOR_READ = new()
        {
            Text = "Paragraph 1.4 of the contract was incorrectly executed",
            RecordId = 1,//TODO real id,
            UserId = 1,//TODO real id,
            UserName = "create1234",
            CreatedAt = DateTime.Now,
        };

        public static CommentForUpdateDto COMMENT_FOR_UPDATE = new()
        {
            Text = "Paragraph 1.5 of the contract was incorrectly executed",
            RecordId = 1,//TODO real id,
            UserId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

    }
}
