using DM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DM.DAL.Interfaces
{
    public interface IProjectRepository<T> : IRepository<Project>
    {
        bool Archive(long? id);
        Task<EntityEntry<Project>> CreateProjectWithUsers(T item);
    }
}
