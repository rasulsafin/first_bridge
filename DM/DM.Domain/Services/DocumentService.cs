using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;
using DM.Domain.Infrastructure;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

using DM.Common.Enums;

namespace DM.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DocumentService> logger)
        {
            Context = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DocumentDto>> GetAll()
        {
            var documents = await Context.Documents.GetAll();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public DocumentDto GetById(long documentId)
        {
            if (documentId < 1) return null;

            var document = Context.Documents.GetById(documentId);
            return _mapper.Map<DocumentDto>(document);
        }

        public async Task<long> Create(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);

            await Context.Documents.Create(document);
            await Context.SaveAsync();

            return document.Id;
        }

        public async Task<bool> Update(DocumentDto documentDto)
        {
            var document = Context.Documents.GetById(documentDto.Id);

            if (document == null) return false;

            document.Name = documentDto.Name;
            document.Description = documentDto.Description;
            document.Location = documentDto.Location;
            document.Object = documentDto.Object;
            document.Photo = documentDto.Photo;
            document.Status = documentDto.Status;
            document.UpdatedAt = DateTime.UtcNow;

            Context.Documents.Update(document);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(long documentId)
        {
            var result = Context.Documents.Delete(documentId);
            await Context.SaveAsync();

            return result;
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.User);

                return action switch
                {
                    ActionEnum.Read => access.Read,
                    ActionEnum.Create => access.Create,
                    ActionEnum.Delete => access.Delete,
                    ActionEnum.Update => access.Update,
                    _ => false,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
