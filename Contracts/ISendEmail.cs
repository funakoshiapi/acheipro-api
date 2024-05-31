using Shared;
using Shared.DataTransferObjects;

namespace Contracts
{
	public interface ISendEmail
	{
		Task SendClientMessageAsync(ClientMessageDto clientMessage);
		Task SendRecoveryPasswordEmail(PasswordRecoveryDto passwordRecoveryDto, Guid RecoveryId);
	}
}

