namespace Todo.Application.CQRS.User.Models
{
    public class RegisterUser : UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}