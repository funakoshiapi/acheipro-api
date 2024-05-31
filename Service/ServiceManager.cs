using System;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
	public sealed class ServiceManager : IServiceManager
	{
		private readonly Lazy<ICompanyService> _companyService;
		private readonly Lazy<IEmployeeService> _employeeService;
		private readonly Lazy<IAuthenticationService> _authenticationService;
		private readonly Lazy<ICompanyImageService> _companyImageService;
        private readonly Lazy<ICompanyDataService> _companyDataService;
		private readonly Lazy<ISendEmail> _sendEmailService;
		private readonly Lazy<IPasswordRecoveryService> _passwordRecoveryService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IWebHostEnvironment environment, IOptions<SmtpSettings> smtpSettings)
		{
			_companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, logger, mapper, userManager));
			_employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, logger, mapper));
            _companyDataService = new Lazy<ICompanyDataService>(() => new CompanyDataService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
				new AuthenticationService(logger, mapper, userManager, configuration));
			_companyImageService = new Lazy<ICompanyImageService>(() => new CompanyImageService(repositoryManager, logger, mapper, environment));
			_sendEmailService = new Lazy<ISendEmail>(() => new SendEmail(smtpSettings, environment, mapper, repositoryManager));
			_passwordRecoveryService = new Lazy<IPasswordRecoveryService>(() => new PasswordRecoveryService(userManager, repositoryManager, logger, mapper));
        }

		public ICompanyService CompanyService => _companyService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;
		public ICompanyDataService CompanyDataService => _companyDataService.Value;
        public ICompanyImageService CompanyImageService => _companyImageService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
		public ISendEmail SendEmailService => _sendEmailService.Value;
		public IPasswordRecoveryService PasswordRecoveryService => _passwordRecoveryService.Value;

    }
}

