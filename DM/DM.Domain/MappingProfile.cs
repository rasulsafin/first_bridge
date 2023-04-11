using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Models;

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

            CreateMap<TemplateModel, TemplateEntity>();
            CreateMap<TemplateEntity, TemplateModel>();

            CreateMap<RecordModel, RecordEntity>();
            CreateMap<RecordEntity, RecordModel>();

            CreateMap<FieldModel, FieldEntity>();
            CreateMap<FieldEntity, FieldModel>();

            CreateMap<FieldModel, FieldEntity>();
            CreateMap<FieldEntity, FieldModel>();

            CreateMap<ListModel, ListEntity>();
            CreateMap<ListEntity, ListModel>();

            CreateMap<ListFieldModel, ListFieldEntity>();
            CreateMap<ListFieldEntity, ListFieldModel>();
        }
    }
}
