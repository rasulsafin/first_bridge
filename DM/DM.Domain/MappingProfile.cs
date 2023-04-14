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
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<ItemEntity, ItemModel>().ReverseMap();
            CreateMap<UserModel, UserProjectEntity>().ReverseMap();
            CreateMap<RecordEntity, RecordModel>().ReverseMap();
            CreateMap<ProjectModel, ProjectEntity>().ReverseMap();
            CreateMap<TemplateModel, TemplateEntity>().ReverseMap();
            CreateMap<CommentModel, CommentEntity>().ReverseMap();
            CreateMap<CommentModelForGet, CommentEntity>().ReverseMap();
            CreateMap<CommentModelForUpdate, CommentEntity>().ReverseMap();
            CreateMap<FieldModel, FieldEntity>().ReverseMap();
            CreateMap<ListFieldModel, ListFieldEntity>().ReverseMap();
            CreateMap<ListModel, ListEntity>().ReverseMap();
            CreateMap<RoleEntity, RoleModel>().ReverseMap();
            CreateMap<PermissionModel, PermissionEntity>().ReverseMap();
        }
    }
}
