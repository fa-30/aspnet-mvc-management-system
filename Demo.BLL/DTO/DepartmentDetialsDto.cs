using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO
{
    public class DepartmentDetialsDto
    {
        public int Id { get; set; } // PK
       
        public int CreatedBy { get; set; } // User Id
        
        public DateOnly CreatedOn { get; set; }
        
        public int LastModifiedBy { get; set; }
       
        public DateOnly LastModifiedOn { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Code { get; set; } = string.Empty;
       
        public string? Description { get; set; }
    }
}
