using DM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class ItemModel
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public ProjectEntity Project { get; set; }
    }
}
