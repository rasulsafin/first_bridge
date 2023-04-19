using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IUserProjectService
    {
        public Task<bool> AddToProject(UserProjectModel userProjectModel);
        public Task<bool> DeleteFromProject(long id);
    }
}
