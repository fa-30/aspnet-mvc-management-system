using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.DepartmentModel
{
    public class BaseEntity
    {

        public int Id { get; set; } // PK

        public int CreatedBy { get; set; } // User Id

        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; } // User ID

        public DateTime LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } // Soft Delete
    }
}
