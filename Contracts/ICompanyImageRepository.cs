using System;
using Entities.Models;

namespace Contracts
{
	public interface ICompanyImageRepository
	{
        void Delete(CompanyImage image);
        void AddImage(CompanyImage image);
        CompanyImage GetCompanyImage(Guid companyId, bool trackChanges);
    }
}

