using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PasswordRecoveryRepository : RepositoryBase<PasswordRecovery>, IPasswordRecoveryRepository
{
    public PasswordRecoveryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void Request(PasswordRecovery passwordRecovery)
    {   
        Create(passwordRecovery);
    }

 public async Task<PasswordRecovery?> GetPasswordRecovery(Guid passwordRecoveryId) =>
			await FindByCondition(c => c.Id.Equals(passwordRecoveryId), false)
			.SingleOrDefaultAsync();


    public void DeleteRequest(PasswordRecovery passwordRecovery)
    {
        Delete(passwordRecovery);
    }

}
