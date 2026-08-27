namespace TaskForge.Models
{
    public class AddTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string Priority { get; set; } = "Medium";
    }
}