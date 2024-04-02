using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public record CompanyDto(Guid Id, string Name, string FullAddress, string Industry, string Role, string Province, string Country, string Address, string ImageName, string telephone, string email, string website);

    public record CompanyForUpdateDto: CompanyForManipulationDto;
    public record CompanyCreationDto: CompanyForManipulationDto;

    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Industry name is a required field.")]
        public string? Industry { get; init; }

        [Required(ErrorMessage = "country name is a required field.")]
        public string? Country { get; init; }

        [Required(ErrorMessage = "Province name is a required field.")]
        public string? Province { get; init; }

        public string? Role { get; init; }

        public string? ImageName { get; init; }

        [Required(ErrorMessage = "Telephone is a required field.")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string? Email { get; set; }

        public string? Website{ get; set; }

       /* public IEnumerable<EmployeeCreationDto>? Employees { get; init; }*/
     
}
}

