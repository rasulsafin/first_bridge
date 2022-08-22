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
            CreateMap<UserEntity, UserModel>().ForMember(a => a.Roles, b => b.MapFrom(c => c.Roles.Name));
            CreateMap<UserModel, UserEntity>();
            CreateMap<ItemModel, ItemEntity>().ReverseMap();

    //        CreateMap<ProjectEntity, ProjectModel>().ForMember(a => a.User, b =>  b.MapFrom(c => c.Users));
    //        CreateMap<ProjectModel, ProjectEntity>().ForMember(a => a.Users, b => b.MapFrom(c => c.User));

            CreateMap<UserModel, UserProjectEntity>().ReverseMap();
            CreateMap<RecordEntity, RecordModel>().ReverseMap();

            CreateMap<ProjectModel, ProjectEntity>();
            CreateMap<ProjectEntity, ProjectModel>();
        }
    }
}
