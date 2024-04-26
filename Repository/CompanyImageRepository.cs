using System;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyImageRepository : RepositoryBase<CompanyImage>, ICompanyImageRepository
    {
        public CompanyImageRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {

        }

        public void Delete(CompanyImage image)
        {
            Delete(image);
        }

        public void AddImage(CompanyImage image)
        {
            Create(image);
        }

        public CompanyImage GetCompanyImage(Guid companyId, bool trackChanges)
        {
            return  FindByCondition(c => c.CompanyId.Equals(companyId), trackChanges).FirstOrDefault();

        }

    }
}

