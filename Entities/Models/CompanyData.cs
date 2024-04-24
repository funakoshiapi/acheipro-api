using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class CompanyData
	{
		[Column("CompanyDataId")]
		public Guid Id { get; set; }

        [Column("CompanyId")]
        public Guid CompanyId { get; set; }

		[MaxLength(1000, ErrorMessage ="Maximum length for the Description is 1000 characters.")]
		public string? CompanyDescription { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum length for the Mission is 1000 characters.")]
		public string? CompanyMission { get; set; }

	}
}

