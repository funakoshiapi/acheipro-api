using System;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
	public interface ICompanyDataService
	{
		Task<CompanyDataDto> GetCompanyDataAsync(Guid companyId, bool trackChanges);
        Task<CompanyDataDto> CreateCompanyDataAsync(CompanyDataCreationDto companyData, Guid companyId);
        Task UpdateCompanyDataAsync(Guid companyId, CompanyDataUpdateDto companyDataForUpdate, bool trackChanges);
        Task DeleteCompanyDataAsync(Guid companyId, bool trackChanges);
       

    }
}

