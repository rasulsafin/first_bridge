using AutoMapper;

using DM.Domain.Infrastructure;

namespace DM.IntegrationTests.Helpers.MockData
{
    public static class MockServiceData
    {
        public static readonly MapperConfiguration TestMapper = new(cfg => cfg.AddProfile<MappingProfile>());

        public static readonly long LARGE_ID = 100000;
        public static readonly long ZERO_ID = 0;
        public static readonly long NEGATIVE_ID = -1;
        public static readonly long POSITIVE_ID = 1;
    }
}
