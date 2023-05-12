using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Logging;

using Xunit;

using DM.Domain.Services;

using DM.DAL.Repositories;

using DM.IntegrationTests.Helpers;
using DM.IntegrationTests.Helpers.MockData;

using DM.Domain.DTO;

namespace DM.IntegrationTests.UnitTests.Services
{
    public class UserProjectUnitTests : IClassFixture<TestDbContext>
    {
        #region Setup Test

        private EFUnitOfWork UnitOfWork { get; set; }
        private static UserProjectService service;
        private static CurrentUserService currentUserService;
        private static readonly ILogger<UserProjectService> logger;

        public UserProjectUnitTests(TestDbContext fixture)
        {
            var _mapper = MockServiceData.TestMapper.CreateMapper();

            UnitOfWork = fixture.UnitOfWork;

            currentUserService = new CurrentUserService(UnitOfWork, _mapper);
            currentUserService.SetCurrentUser(1);

            service = new UserProjectService(UnitOfWork, _mapper);
        }

        #endregion

        #region Get Testing

        #endregion

        #region Create Testing

        #endregion

        #region Delete Testing


        #endregion
    }
}
