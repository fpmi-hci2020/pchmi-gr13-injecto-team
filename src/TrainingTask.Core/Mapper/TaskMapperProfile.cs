using AutoMapper;

using TrainingTask.Common.Contract.Task;
using TrainingTask.Common.DTO;

namespace TrainingTask.Core.Mapper
{
    public class TaskMapperProfile : Profile
    {
        public TaskMapperProfile()
        {
            CreateMap<CreateTaskRequest, Task>();
            CreateMap<EditTaskRequest, Task>();
            CreateMap<DeleteTaskRequest, Task>();
        }
    }
}