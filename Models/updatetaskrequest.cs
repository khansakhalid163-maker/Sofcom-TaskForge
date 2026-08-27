namespace TaskForge.Models
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedToUserId { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}