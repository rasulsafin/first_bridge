using DM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Interfaces
{
    public interface IDocumentRepository<T> : IRepository<Document>
    {
    }
}
