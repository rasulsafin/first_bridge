using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Models;
using System.Linq;

namespace DM.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseModel>().ReverseMap();

            //Organization map
            CreateMap<OrganizationEntity, OrganizationModel>().ReverseMap();
            CreateMap<OrganizationEntity, OrganizationForCreateModel>().ReverseMap();
            CreateMap<OrganizationEntity, OrganizationForUpdateModel>().ReverseMap();

            //User map
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<UserEntity, UserForReadModel>().ForMember(n => n.Projects, m => m.MapFrom(o => o.UserProjects.Select(u => u.Project))).ReverseMap();
            CreateMap<UserEntity, UserForCreateModel>().ReverseMap();
            CreateMap<UserEntity, UserForUpdateModel>().ReverseMap();

            //Project map
            CreateMap<ProjectEntity, ProjectModel>().ReverseMap();
            CreateMap<ProjectEntity, ProjectForReadModel>().ForMember(n => n.Users, m => m.MapFrom(o => o.UserProjects.Select(u => u.User))).ReverseMap();
            CreateMap<ProjectEntity, ProjectForUpdateModel>().ReverseMap();

            //User&Project map
            CreateMap<UserProjectEntity, UserProjectModel>().ReverseMap();

            //Role map
            CreateMap<RoleEntity, RoleModel>().ReverseMap();
            CreateMap<RoleEntity, RoleForCreateModel>().ReverseMap();
            CreateMap<RoleEntity, RoleForUpdateModel>().ReverseMap();

            //Comment map
            CreateMap<CommentEntity, CommentModel>().ReverseMap();
            CreateMap<CommentEntity, CommentForReadModel>().ReverseMap();
            CreateMap<CommentEntity, CommentModelForUpdate>().ReverseMap();

            //Template map
            CreateMap<TemplateEntity, TemplateModel>().ReverseMap();
            CreateMap<TemplateEntity, TemplateForUpdateModel>().ReverseMap();

            //Record map
            CreateMap<RecordEntity, RecordModel>().ReverseMap();
            CreateMap<RecordEntity, RecordForReadModel>().ReverseMap();
            CreateMap<RecordEntity, RecordForCreateModel>().ReverseMap();

            //Fields map
            CreateMap<FieldEntity, FieldModel>().ReverseMap();
            CreateMap<ListFieldEntity, ListFieldModel>().ReverseMap();
            CreateMap<ListEntity, ListModel>().ReverseMap();

            CreateMap<ItemEntity, ItemModel>().ReverseMap();
            CreateMap<PermissionModel, PermissionEntity>().ReverseMap();
        }
    }
}
