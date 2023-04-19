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
            CreateMap<OrganizationEntity, OrganizationModel>().ReverseMap();

            //user map
            CreateMap<UserEntity, UserModel>().ForMember(n => n.Projects, m => m.MapFrom(o => o.UserProjects.Select(u => u.Project))).ReverseMap();
            CreateMap<UserEntity, UserForCreateModel>().ReverseMap();
            CreateMap<UserEntity, UserForUpdateModel>().ReverseMap();

            //project map
            CreateMap<ProjectEntity, ProjectModel>().ForMember(n => n.Users, m => m.MapFrom(o => o.UserProjects.Select(u => u.User))).ReverseMap();
            CreateMap<ProjectEntity, ProjectForUpdateModel>().ReverseMap();

            //user&project map
            CreateMap<UserProjectEntity, UserProjectModel>().ReverseMap();

            CreateMap<ItemEntity, ItemModel>().ReverseMap();
            CreateMap<RecordEntity, RecordModel>().ReverseMap();
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
