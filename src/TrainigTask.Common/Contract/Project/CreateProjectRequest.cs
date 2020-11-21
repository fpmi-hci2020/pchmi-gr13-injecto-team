namespace TrainingTask.Common.Contract.Project
{
    public class CreateProjectRequest : BaseRequest
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}