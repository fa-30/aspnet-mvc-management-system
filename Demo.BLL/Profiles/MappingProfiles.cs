using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.DTO.EmployeeDtos;
using Demo.DAL.Models.EmployeeModel;

namespace Demo.BLL.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.EmpGender, Options => Options.MapFrom(src => src.Gender))
            .ForMember(dest => dest.EmpType, options => options.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department != null?src.Department.Name : null))
            .ForMember(dest => dest.Image, options => options.MapFrom(src => src.ImageName));

            CreateMap<Employee, EmployeeDetailsDto>()
            .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
            .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
            .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department != null ? src.Department.Name : null))
            .ForMember(dest => dest.Image, options => options.MapFrom(src => src.ImageName));

            CreateMap<CreatedEmployeeDto, Employee>()
            .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            CreateMap<UpdatedEmployeeDto, Employee>()
            .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
