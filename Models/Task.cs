namespace TaskAPI.Models
{
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string? Title { get; set; }
        public virtual bool IsComplete { get; set; }

    }
}
