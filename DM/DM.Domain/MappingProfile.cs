using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Models;
using DM.Entities;

namespace DM.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
            CreateMap<ItemModel, ItemEntity>();
            CreateMap<ItemEntity, ItemModel>();
            CreateMap<ProjectEntity, ProjectModel>();
            CreateMap<ProjectModel, ProjectEntity>();
            CreateMap<RecordEntity, RecordModel>();
            CreateMap<RecordModel, RecordEntity>();
        }
    }
}
