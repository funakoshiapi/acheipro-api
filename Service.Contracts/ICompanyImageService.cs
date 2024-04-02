using System;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
	public interface ICompanyImageService
	{
        Task<Tuple<int, string>> SaveImageToResources(IFormFile imageFile);
        Task AddImageModel(Guid companyId, CompanyImageDto image, bool trackChanges);
        Task<CompanyImageDto> GetImageName(Guid companyId, bool trackChanges);
        Task UpdateImage(CompanyImageDto model, bool trackChanges);
        Task<Company> CheckIfCompanyExists(Guid companyId, bool trackChanges);
    }
}

