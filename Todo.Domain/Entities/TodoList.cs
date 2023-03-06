using Todo.Domain.Enums;

namespace Todo.Domain.Entities
{
    public class TodoList : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Remarks { get; set; }
        public TodoStatusType Status { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DateCompleted { get; set; }
        public User User { get; set; }
    }
}