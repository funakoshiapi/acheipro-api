using System;
using System.ComponentModel.Design;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
	public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
	{
		public CompanyRepository( RepositoryContext repositoryContext)
			:base(repositoryContext)
		{
		} 

		public void DeleteCompany(Company company)
		{
			Delete(company);
		}

        public void CreateCompany(Company company)
        {
			Create(company);
        }

        public async Task<PagedList<Company>> GetAllCompaniesAsync(bool trackChanges, CompanyParameters companyParameters)
		{
			if(!String.IsNullOrEmpty(companyParameters.Province) && !String.IsNullOrEmpty(companyParameters.Industry))
			{
                var companies = await FindByCondition(c => c.Province.ToUpper().Contains(companyParameters.Province.ToUpper()) && c.Industry.ToUpper().Contains(companyParameters.Industry.ToUpper()), trackChanges)
				.Sort(companyParameters.OrderBy)
                .Skip((companyParameters.PageNumber - 1) * companyParameters.PageSize)
                .Take(companyParameters.PageSize)
                .ToListAsync();

                var count = await FindByCondition(c => c.Province.ToUpper().Equals(companyParameters.Province.ToUpper()) && c.Industry.ToUpper().Equals(companyParameters.Industry.ToUpper()), trackChanges).CountAsync();

                return new PagedList<Company>(companies, count, companyParameters.PageNumber, companyParameters.PageSize);
            }


            if (!String.IsNullOrEmpty(companyParameters.Province))
            {
                var companies = await FindByCondition(c => c.Province.ToUpper().Equals(companyParameters.Province.ToUpper()), trackChanges)
                .Sort(companyParameters.OrderBy)
                .Skip((companyParameters.PageNumber - 1) * companyParameters.PageSize)
                .Take(companyParameters.PageSize)
                .ToListAsync();

                var count = await FindByCondition(c => c.Province.ToUpper().Equals(companyParameters.Province.ToUpper()), trackChanges).CountAsync();

                return new PagedList<Company>(companies, count, companyParameters.PageNumber, companyParameters.PageSize);
            }

            if (!String.IsNullOrEmpty(companyParameters.Industry))
            {
                var companies =  await FindByCondition(c => c.Industry.ToUpper().Equals(companyParameters.Industry.ToUpper()), trackChanges)
                .Sort(companyParameters.OrderBy)
                .ToListAsync();

                var count = await FindByCondition(c => c.Industry.ToUpper().Equals(companyParameters.Industry.ToUpper()), trackChanges).CountAsync();

                return new PagedList<Company>(companies, count, companyParameters.PageNumber, companyParameters.PageSize);
            }

            var companyResult = await FindAll(trackChanges)
             .Sort(companyParameters.OrderBy)
             .Skip((companyParameters.PageNumber - 1) * companyParameters.PageSize)
             .Take(companyParameters.PageSize)
             .ToListAsync();

            var countResult = await FindAll(trackChanges).CountAsync();

            return new PagedList<Company>(companyResult, countResult, companyParameters.PageNumber, companyParameters.PageSize);

        }


        public async Task <IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
			await FindByCondition(x => ids.Contains(x.Id), trackChanges)
			.ToListAsync();

        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>
			await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
			.SingleOrDefaultAsync();

    }
}

