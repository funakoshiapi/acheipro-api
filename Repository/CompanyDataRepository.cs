using System;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class CompanyDataRepository : RepositoryBase<CompanyData>, ICompanyDataRepository
	{
		public CompanyDataRepository( RepositoryContext repositoryContext)
			:base(repositoryContext)
		{
		}

        public void CreateCompanyData(CompanyData companyData)
        {
            Create(companyData);
        }


        public void DeleteCompanyData(CompanyData companyData)
        {
            Delete(companyData);
        }

        public async  Task<CompanyData> GetCompanyDataByIdAsync(Guid companyId, bool trackChanges)
        {
            return await FindByCondition(x => x.CompanyId.Equals(companyId), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
