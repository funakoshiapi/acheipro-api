using System;
namespace Contracts
{
	public interface IRepositoryManager
	{
		ICompanyRepository Company { get; }
		IEmployeeRepository Employee { get; }
		ICompanyDataRepository CompanyData { get; }
        ICompanyImageRepository CompanyImage {get; }
		Task SaveAsync();
	}
}

