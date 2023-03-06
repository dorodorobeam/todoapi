using Todo.Domain.Enums;

namespace Todo.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        private string _firstname;
        private string _lastname;
        private string _middlename;

        public string? Prefix { get; set; }
        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; setNames(); }
        }
        public string? Middlename
        {
            get { return _middlename; }
            set { _middlename = value; setNames(); }
        }
        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; setNames(); }
        }
        public string? Suffix { get; set; }
        public string Fullname { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public GenderType? Gender { get; set; }
        public CivilStatusType CivilStatus { get; set; }
        public string? Specialization { get; set; }

        private void setNames()
        {
            Fullname = $"{Prefix} {Firstname} {Middlename} {Lastname} {Suffix}";
            Name = $"{Prefix} {Lastname} {Firstname} {Suffix} {Middlename}";
        }
    }
}