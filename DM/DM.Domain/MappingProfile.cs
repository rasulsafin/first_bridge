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

            CreateMap<RecordEntity, RecordModel>();
            CreateMap<RecordModel, RecordEntity>();

            CreateMap<ProjectModel, ProjectEntity>();
            CreateMap<ProjectEntity, ProjectModel>();

            CreateMap<TemplateModel, TemplateEntity>();
            CreateMap<TemplateEntity, TemplateModel>();

            CreateMap<CommentModel, CommentEntity>();
            CreateMap<CommentEntity, CommentModel>();

            CreateMap<CommentModelForGet, CommentEntity>();
            CreateMap<CommentEntity, CommentModelForGet>();

            CreateMap<CommentModelForUpdate, CommentEntity>();
            CreateMap<CommentEntity, CommentModelForUpdate>();

            CreateMap<FieldModel, FieldEntity>();
            CreateMap<FieldEntity, FieldModel>();

            CreateMap<ListFieldModel, ListFieldEntity>();
            CreateMap<ListFieldEntity, ListFieldModel>();

            CreateMap<ListModel, ListEntity>();
            CreateMap<ListEntity, ListModel>();
        }
    }
}
