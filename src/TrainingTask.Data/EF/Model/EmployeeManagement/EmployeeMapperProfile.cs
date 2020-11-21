using System.Linq;

using AutoMapper;

using EmployeeEF = TrainingTask.Data.EF.Model.EmployeeManagement.Employee;
using EmployeeModel = TrainingTask.Common.DTO.Employee;

namespace TrainingTask.Data.EF.Model.EmployeeManagement
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeModel, EmployeeEF>()
                .ReverseMap();
        }
    }
}