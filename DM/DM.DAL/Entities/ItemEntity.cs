using DM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class ItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public ProjectEntity Project { get; set; }
    }
}
