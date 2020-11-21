using System;

using AutoMapper;

using TrainingTask.Common.Contract.Task;
using TrainingTask.Common.DTO;
using TrainingTask.Data;

namespace TrainingTask.Core.Service
{
    public class TaskService
    {
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public GetAllTasksResponse GetAllTasks(GetAllTasksRequest request, UnitOfWork context)
        {
            var response = new GetAllTasksResponse
            {
                Tasks = context.Tasks.GetAllItems()
            };

            return response;
        }

        public GetTaskResponse GetTask(GetTaskRequest request, UnitOfWork context)
        {
            var response = new GetTaskResponse
            {
                Task = context.Tasks.GetItem(request.Id)
            };

            return response;
        }

        public CreateTaskResponse CreateTask(CreateTaskRequest task, UnitOfWork context)
        {
            var response = new CreateTaskResponse
            {
                Id = context.Tasks.AddItem(_mapper.Map<Task>(task))
            };

            return response;
        }

        public EditTaskResponse EditTask(EditTaskRequest task, UnitOfWork context)
        {
            var response = new EditTaskResponse
            {
                Count = context.Tasks.UpdateItem(_mapper.Map<Task>(task))
            };

            return response;
        }

        public DeleteTaskResponse DeleteTask(DeleteTaskRequest task, UnitOfWork context)
        {
            var response = new DeleteTaskResponse();

            var taskDb = context.Tasks.GetItem(task.Id);

            response.Count = context.Tasks.RemoveItem(taskDb);

            return response;
        }
    }
}