using System;
namespace Contracts
{
	public interface IRepositoryManager
	{
		ICompanyRepository Company { get; }
		IEmployeeRepository Employee { get; }
		ICompanyDataRepository CompanyData { get; }
        ICompanyImageRepository CompanyImage {get; }
		IClientMessageRepository ClientMessage { get; }
		IPasswordRecoveryRepository PasswordRecovery{ get; }
		Task SaveAsync();
	}
}

