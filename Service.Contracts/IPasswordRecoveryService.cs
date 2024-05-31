using Entities;
using Shared;

namespace Service.Contracts;

public interface IPasswordRecoveryService
{
    public Task<bool> UpdatePassword(PasswordRecoveryDto passwordRecovery, Guid requestId);
    public Task<PasswordRecovery?> GeneratePasswordRequest(PasswordRecoveryDto passwordRecovery);

    public Task DeleteRequest(Guid Id);
}
