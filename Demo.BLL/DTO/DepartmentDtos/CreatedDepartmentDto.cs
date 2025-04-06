using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO.DepartmentDtos
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required !!!!")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(100, int.MaxValue)]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateOnly DateOfCreation { get; set; }
    }
}
