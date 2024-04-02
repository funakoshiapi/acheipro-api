using System;
using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects
{
	public record CompanyImageDto
	{
		public Guid CompanyId { get; set; }
		public IFormFile? ImageFile { get; set; }
		public string? ImageName { get; set; }
	}
}

