using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public record UserForRegistrationDto
	{
        [Required(ErrorMessage = "Nome é requerido")]
        public string? FirstName { get; init; }
        [Required(ErrorMessage = "Sobrenome é requerido")]
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Nome de Usuario é requerido")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password é requerido")]
        public string? Password { get; init; }
        [Required(ErrorMessage = "Email é requerido")]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Numero de telefone é requerido")]
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }
    }
}

