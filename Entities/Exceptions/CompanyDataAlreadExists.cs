using System;
namespace Entities.Exceptions
{
	public sealed class CompanyDataAlreadExists : BadRequestException
	{
		public CompanyDataAlreadExists(Guid id)
			:base($"Company data already exist for company with id:{id}")
		{
		}
	}
}

