using System;
using Entities.Models;

namespace Contracts
{
	public interface ICompanyDataRepository
	{
        Task<CompanyData> GetCompanyDataByIdAsync(Guid companyId, bool trackChanges);
        void CreateCompanyData(CompanyData companyData);
        void DeleteCompanyData(CompanyData companyData);
    }
}

