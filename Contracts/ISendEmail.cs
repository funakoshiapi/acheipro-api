using Shared.DataTransferObjects;

namespace Contracts
{
	public interface ISendEmail
	{
		Task SendClientMessageAsync(ClientMessageDto clientMessage);
	}
}

