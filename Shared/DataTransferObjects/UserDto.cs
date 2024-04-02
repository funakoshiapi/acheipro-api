using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public record UserDto
	{
		[Required(ErrorMessage="User name is required")]
		public string? UserName { get; init; }
		public Guid? CompanyId { get; init; }
	}
}

