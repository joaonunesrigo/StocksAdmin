﻿using System.ComponentModel.DataAnnotations;

namespace StocksAdmin.Communication.Requests.User
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = "";
    }
}
