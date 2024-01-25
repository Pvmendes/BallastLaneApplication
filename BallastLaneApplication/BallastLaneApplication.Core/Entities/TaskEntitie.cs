namespace BallastLaneApplication.Core.Entities
{
    public class TaskEntitie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
