using Todo.Domain.Enums;

namespace Todo.Application.CQRS.User.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Prefix { get; set; }
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public string? Suffix { get; set; }
        public string Fullname { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public GenderType? Gender { get; set; }
        public CivilStatusType CivilStatus { get; set; }
        public string? Specialization { get; set; }
    }
}