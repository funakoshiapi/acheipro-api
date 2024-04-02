using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public record CompanyDataDto(Guid Id, Guid CompanyId, string CompanyDescription, string CompanyMission);

    public record CompanyDataUpdateDto : CompanyForDataManipulation;
    public record CompanyDataCreationDto : CompanyForDataManipulation;

    public abstract record CompanyForDataManipulation
	{
        [Required(ErrorMessage = "Company Description is a required field.")]
        [MaxLength(1000, ErrorMessage = "Maximum length for the Description is 1000 characters.")]
        public string? CompanyDescription { get; set; }

        [Required(ErrorMessage = "Company Mission is a required field.")]
        [MaxLength(1000, ErrorMessage = "Maximum length for the Mission is 1000 characters.")]
        public string? CompanyMission { get; set; }
    }
}

