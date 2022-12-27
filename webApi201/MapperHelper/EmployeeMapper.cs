
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi201.Entity;
using webApi201.Model;

namespace webApi201.MapperHelper
{
    public class EmployeeMapper:Profile
    {
        public EmployeeMapper()
        {

            CreateMap<EmployeeModel, Employee>().ReverseMap();
        }
    }
}
