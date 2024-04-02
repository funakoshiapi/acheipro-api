﻿using System;
using Contracts;

namespace Repository
{
	public class RepositoryManager : IRepositoryManager
	{
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<ICompanyImageRepository> _companyImageRepository;
        private readonly Lazy<ICompanyDataRepository> _companyDataRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
		{
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
            _companyImageRepository = new Lazy<ICompanyImageRepository>(() => new CompanyImageRepository(repositoryContext));
            _companyDataRepository = new Lazy<ICompanyDataRepository>(() => new CompanyDataRepository(repositoryContext));

        }

        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public ICompanyImageRepository CompanyImage => _companyImageRepository.Value;
        public ICompanyDataRepository CompanyData => _companyDataRepository.Value;
        public async Task SaveAsync() => await  _repositoryContext.SaveChangesAsync();
    }
}

