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
            CreateMap<OrganizationEntity, OrganizationModel>().ReverseMap();

            CreateMap<UserEntity, UserModel>().ForMember(a => a.Roles, b => b.MapFrom(c => c.Roles));
            CreateMap<UserModel, UserEntity>();
            CreateMap<ItemModel, ItemEntity>().ReverseMap();

            CreateMap<UserModel, UserProjectEntity>().ReverseMap();
            CreateMap<RecordEntity, RecordModel>().ReverseMap();

            CreateMap<ProjectModel, ProjectEntity>();
            CreateMap<ProjectEntity, ProjectModel>();
        }
    }
}
