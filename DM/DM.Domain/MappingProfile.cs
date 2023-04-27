using System.Linq;

using AutoMapper;

using DM.DAL.Entities;

using DM.Domain.Models;

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
            CreateMap<OrganizationEntity, OrganizationForUpdateModel>().ForMember(n => n.UserIds, m => m.MapFrom(o => o.Users.Select(u => u.Id)))
                                                                       .ForMember(n => n.ProjectIds, m => m.MapFrom(o => o.Projects.Select(u => u.Id)))
                                                                       .ReverseMap();

            //User map
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<UserEntity, UserForReadModel>().ForMember(n => n.Projects, m => m.MapFrom(o => o.UserProjects.Select(u => u.Project)))
                                                     .ForMember(n => n.ProjectsIds, m => m.MapFrom(o => o.UserProjects.Select(u => u.ProjectId)))
                                                     .ReverseMap();
            CreateMap<UserEntity, UserForCreateModel>().ReverseMap();
            CreateMap<UserEntity, UserForUpdateModel>().ReverseMap();

            //Project map
            CreateMap<ProjectEntity, ProjectModel>().ReverseMap();
            CreateMap<ProjectEntity, ProjectForReadModel>().ForMember(n => n.Users, m => m.MapFrom(o => o.UserProjects.Select(u => u.User)))
                                                           .ForMember(n => n.UserIds, m => m.MapFrom(o => o.UserProjects.Select(u => u.UserId)))
                                                           .ForMember(n => n.ItemIds, m => m.MapFrom(o => o.Items.Select(u => u.Id)))
                                                           .ReverseMap();
            CreateMap<ProjectEntity, ProjectForUpdateModel>().ReverseMap();

            //User&Project map
            CreateMap<UserProjectEntity, UserProjectModel>().ReverseMap();

            //Role map
            CreateMap<RoleEntity, RoleModel>().ForMember(n => n.PermissionIds, m => m.MapFrom(o => o.Permissions.Select(u => u.Id)))
                                              .ForMember(n => n.UserIds, m => m.MapFrom(o => o.Permissions.Select(u => u.Id)))
                                              .ReverseMap();
            CreateMap<RoleEntity, RoleForCreateModel>().ForMember(n => n.PermissionIds, m => m.MapFrom(o => o.Permissions.Select(u => u.Id)))
                                                       .ReverseMap();
            CreateMap<RoleEntity, RoleForUpdateModel>().ReverseMap();

            //Comment map
            CreateMap<CommentEntity, CommentModel>().ReverseMap();
            CreateMap<CommentEntity, CommentForReadModel>().ReverseMap();
            CreateMap<CommentEntity, CommentForUpdateModel>().ReverseMap();

            //Template map
            CreateMap<TemplateEntity, TemplateModel>().ReverseMap();
            CreateMap<TemplateEntity, TemplateForCreateModel>().ForMember(n => n.FieldIds, m => m.MapFrom(o => o.Fields.Select(u => u.Id)))
                                                               .ForMember(n => n.ListFieldIds, m => m.MapFrom(o => o.ListFields.Select(u => u.Id)))
                                                               .ReverseMap();
            CreateMap<TemplateEntity, TemplateForUpdateModel>().ReverseMap();

            //Record map
            CreateMap<RecordEntity, RecordModel>().ForMember(n => n.FieldIds, m => m.MapFrom(o => o.Fields.Select(u => u.Id)))
                                                  .ForMember(n => n.ListFieldIds, m => m.MapFrom(o => o.ListFields.Select(u => u.Id)))
                                                  .ReverseMap();
            CreateMap<RecordEntity, RecordForReadModel>().ForMember(n => n.CommentIds, m => m.MapFrom(o => o.Comments.Select(u => u.Id)))
                                                         .ReverseMap();
            CreateMap<RecordEntity, RecordForCreateModel>().ReverseMap();

            //Document map
            CreateMap<DocumentEntity, DocumentModel>().ReverseMap();

            //Fields map
            CreateMap<FieldEntity, FieldModel>().ReverseMap();
            CreateMap<ListFieldEntity, ListFieldModel>().ForMember(n => n.ListIds, m => m.MapFrom(o => o.Lists.Select(u => u.Id)))
                                                        .ReverseMap();
            CreateMap<ListEntity, ListModel>().ReverseMap();

            CreateMap<ItemEntity, ItemModel>().ReverseMap();
            CreateMap<PermissionModel, PermissionEntity>().ReverseMap();
        }
    }
}
