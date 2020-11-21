using AutoMapper;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.DTO;

namespace TrainingTask.Core.Mapper
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<EditEmployeeRequest, Employee>();
            CreateMap<DeleteEmployeeRequest, Employee>();
        }
    }
}