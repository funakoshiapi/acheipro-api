using System;
namespace Shared.DataTransferObjects
{
	public record ClientMessageDto(Guid CompanyId, string Topic, string FirstName, string LastName, string PhoneNumber, string Email, string Message, string Interest, string RecipientCompanyName, string RecipientCompanyEmail );
	
}

