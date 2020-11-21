using System.Collections.Generic;

namespace TrainingTask.Common.Contract.Employee
{
    public class GetAllEmployeesResponse : BaseResponse
    {
        public IEnumerable<DTO.Employee> Employees { get; set; }
    }
}