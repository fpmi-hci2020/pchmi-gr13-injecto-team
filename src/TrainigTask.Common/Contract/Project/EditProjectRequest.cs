namespace TrainingTask.Common.Contract.Project
{
    public class EditProjectRequest : BaseRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}