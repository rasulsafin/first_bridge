using System.Linq;

using AutoMapper;

using DM.Domain.DTO;

using DM.DAL.Entities;

namespace DM.Domain.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseDto>().ReverseMap();

            //Organization map
            CreateMap<Organization, OrganizationDto>().ReverseMap();
            CreateMap<Organization, OrganizationForCreateDto>().ReverseMap();
            CreateMap<Organization, OrganizationForUpdateDto>().ForMember(n => n.UserIds, m => m.MapFrom(o => o.Users.Select(u => u.Id)))
                                                                       .ForMember(n => n.ProjectIds, m => m.MapFrom(o => o.Projects.Select(u => u.Id)))
                                                                       .ReverseMap();

            //User map
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserForReadDto>().ForMember(n => n.Projects, m => m.MapFrom(o => o.UserProjects.Select(u => u.Project)))
                                                     .ForMember(n => n.ProjectsIds, m => m.MapFrom(o => o.UserProjects.Select(u => u.ProjectId)))
                                                     .ReverseMap();
            CreateMap<User, UserForCreateDto>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();

            //Project map
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectForReadDto>().ForMember(n => n.Users, m => m.MapFrom(o => o.UserProjects.Select(u => u.User)))
                                                           .ForMember(n => n.UserIds, m => m.MapFrom(o => o.UserProjects.Select(u => u.UserId)))
                                                           .ForMember(n => n.ItemIds, m => m.MapFrom(o => o.Items.Select(u => u.Id)))
                                                           .ForMember(n => n.TemplateIds, m => m.MapFrom(o => o.Templates.Select(u => u.Id)))
                                                           .ReverseMap();
            CreateMap<Project, ProjectForUpdateDto>().ReverseMap();

            //User&Project map
            CreateMap<UserProject, UserProjectDto>().ReverseMap();

            //Role map
            CreateMap<Role, RoleDto>().ForMember(n => n.PermissionIds, m => m.MapFrom(o => o.Permissions.Select(u => u.Id)))
                                              .ForMember(n => n.UserIds, m => m.MapFrom(o => o.Permissions.Select(u => u.Id)))
                                              .ReverseMap();
            CreateMap<Role, RoleForCreateDto>().ForMember(n => n.PermissionIds, m => m.MapFrom(o => o.Permissions.Select(u => u.Id)))
                                                       .ReverseMap();
            CreateMap<Role, RoleForUpdateDto>().ReverseMap();

            //Comment map
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CommentForReadDto>().ReverseMap();
            CreateMap<Comment, CommentForUpdateDto>().ReverseMap();

            //Template map
            CreateMap<Template, TemplateDto>().ReverseMap();
            CreateMap<Template, TemplateForReadDto>().ForMember(n => n.FieldIds, m => m.MapFrom(o => o.Fields.Select(u => u.Id)))
                                                   .ForMember(n => n.ListFieldIds, m => m.MapFrom(o => o.ListFields.Select(u => u.Id)))
                                                   .ReverseMap();
            CreateMap<Template, TemplateForCreateDto>().ForMember(n => n.FieldIds, m => m.MapFrom(o => o.Fields.Select(u => u.Id)))
                                                               .ForMember(n => n.ListFieldIds, m => m.MapFrom(o => o.ListFields.Select(u => u.Id)))
                                                               .ReverseMap();
            CreateMap<Template, TemplateForUpdateDto>().ReverseMap();

            //Record map
            CreateMap<Record, RecordDto>().ForMember(n => n.FieldIds, m => m.MapFrom(o => o.Fields.Select(u => u.Id)))
                                                  .ForMember(n => n.ListFieldIds, m => m.MapFrom(o => o.ListFields.Select(u => u.Id)))
                                                  .ReverseMap();
            CreateMap<Record, RecordForReadDto>().ForMember(n => n.CommentIds, m => m.MapFrom(o => o.Comments.Select(u => u.Id)))
                                                         .ReverseMap();
            CreateMap<Record, RecordForCreateDto>().ReverseMap();

            //Document map
            CreateMap<Document, DocumentDto>().ReverseMap();

            //Fields map
            CreateMap<Field, FieldDto>().ReverseMap();
            CreateMap<ListField, ListFieldDto>().ForMember(n => n.ListIds, m => m.MapFrom(o => o.Lists.Select(u => u.Id)))
                                                        .ReverseMap();
            CreateMap<List, ListDto>().ReverseMap();

            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<PermissionDto, Permission>().ReverseMap();
        }
    }
}
