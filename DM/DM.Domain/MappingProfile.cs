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
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<ItemModel, ItemEntity>().ReverseMap();
            CreateMap<ProjectEntity, ProjectModel>().ReverseMap();
            CreateMap<RecordEntity, RecordModel>().ReverseMap();
            CreateMap<FieldsEntity, FieldsModel>().ReverseMap();
        }
    }
}
