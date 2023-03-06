namespace Todo.Domain
{
    public abstract class BaseEntity<T> where T : struct
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedById { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}