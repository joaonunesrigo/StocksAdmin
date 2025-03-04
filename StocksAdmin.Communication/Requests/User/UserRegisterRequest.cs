using System.ComponentModel.DataAnnotations;

namespace ProjectClientHub.Communication.Requests.User
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string UserLastName { get; set; } = "";

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = "";

        [Compare("Password", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
