using System;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
	public interface ICompanyService
	{
		Task<(IEnumerable<CompanyDto> companies, MetaData metaData)> GetAllCompaniesAsync(bool trackChanges, CompanyParameters companyParameters);
		Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
		Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto company);
		Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
		Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyCreationDto> companyCollectiion);
		Task DeleteCompanyAsync (Guid companyId, bool trackchanges);
		Task UpdateCompanyAsync (Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
		
	}
}

