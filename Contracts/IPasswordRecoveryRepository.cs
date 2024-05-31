using Entities;

namespace Contracts;

public interface IPasswordRecoveryRepository
{
    public void Request(PasswordRecovery passwordRecovery);

    public Task<PasswordRecovery?> GetPasswordRecovery(Guid passwordRecoveryId);

    public void DeleteRequest(PasswordRecovery passwordRecovery);
}
