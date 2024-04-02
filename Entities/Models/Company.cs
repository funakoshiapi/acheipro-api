using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class Company
	{
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Company industry is a required field.")]
        public string? Industry { get; set; }

        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? Role { get; set; }
        public string? ImageName { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        public ICollection<Employee>? Employees { get; set;}
      
    }
}

