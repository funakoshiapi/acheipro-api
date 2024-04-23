using System;
using Contracts;

namespace Service.Contracts
{
	public interface IServiceManager
	{
		ICompanyService CompanyService { get; }
		IEmployeeService EmployeeService { get;  }
		ICompanyImageService CompanyImageService { get; }
		ICompanyDataService CompanyDataService { get; }
		IAuthenticationService AuthenticationService { get; }
        ISendEmail SendEmailService { get; }
    }
}

