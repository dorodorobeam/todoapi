﻿namespace Todo.Application.CQRS.User.Models
{
    public class LoginModel
    {
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}