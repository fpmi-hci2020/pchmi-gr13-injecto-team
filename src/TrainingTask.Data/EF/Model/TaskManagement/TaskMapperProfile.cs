using System.Linq;

using AutoMapper;

using TaskEF = TrainingTask.Data.EF.Model.TaskManagement.Task;
using TaskModel = TrainingTask.Common.DTO.Task;

namespace TrainingTask.Data.EF.Model.TaskManagement
{
    public class TaskMapperProfile : Profile
    {
        public TaskMapperProfile()
        {
            CreateMap<TaskEF, TaskModel>()
                .ReverseMap();
        }
    }
}