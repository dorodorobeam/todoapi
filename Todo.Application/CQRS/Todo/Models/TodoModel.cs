using Todo.Application.CQRS.User.Models;
using Todo.Domain.Enums;

namespace Todo.Application.CQRS.Todo.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Remarks { get; set; }
        public TodoStatusType Status { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int UserId { get; set; }
    }
}