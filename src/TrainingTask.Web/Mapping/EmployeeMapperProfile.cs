using AutoMapper;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.DTO;
using TrainingTask.Web.Model;
using TrainingTask.Web.Model.WebApi.Employee;

namespace TrainingTask.Web.Mapping
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<UpdateEmployeeItem, CreateEmployeeRequest>();
            CreateMap<UpdateEmployeeItem, DeleteEmployeeRequest>();
            CreateMap<UpdateEmployeeItem, EditEmployeeRequest>();
            CreateMap<Employee, EmployeeListViewModel>();
            CreateMap<EmployeeListViewModel, CreateEmployeeRequest>();
            CreateMap<EmployeeListViewModel, EditEmployeeRequest>();
        }
    }
}