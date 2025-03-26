using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTO;
using Demo.DAL.Models;

namespace Demo.BLL.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                DeptId = D.Id,
                Code = D.Code,
                Description = D.Description,
                Name = D.Name,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }
        public static DepartmentDetialsDto ToDepartmentDetialsDto(this Department D)
        {
            return new DepartmentDetialsDto()
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                CreatedOn = DateOnly.FromDateTime(D.CreatedOn),
                CreatedBy = D.CreatedBy,
                LastModifiedBy = D.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(D.LastModifiedOn),
                IsDeleted = D.IsDeleted
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(time: new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
        {
            Id = departmentDto.Id,
            Name = departmentDto.Name,
            Code = departmentDto.Code,
            Description = departmentDto.Description,
            CreatedOn = departmentDto.DateOfCreation.ToDateTime(time: new TimeOnly())
        };


    }
}
