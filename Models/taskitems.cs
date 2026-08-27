namespace TaskForge.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string Priority { get; set; }   // Low, Medium, High
        public string Status { get; set; }     // Pending, InProgress, Done
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}