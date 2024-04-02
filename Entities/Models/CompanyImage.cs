using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Entities.Models
{
	public class CompanyImage
	{
        [Column("ImageId")]
        public Guid Id { get; set; }

        [Column("CompanyId")]
        public Guid CompanyId { get; set; }

        public string? ImageName { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }
	}
}

