using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<EmployeeToAddDto, Employee>();
            CreateMap<Employee, EmployeeToAddDto>();

            CreateMap<EmployeeToDeleteDto, Employee>();
            CreateMap<Employee, EmployeeToDeleteDto>();

            CreateMap<EmployeeToUpdateDto, Employee>();
            CreateMap<Employee, EmployeeToUpdateDto>();

            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();

            CreateMap<ListDto, Employee>();
            CreateMap<Employee, ListDto>();
        }
    }
}
