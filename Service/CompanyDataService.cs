using System;
using System.ComponentModel.Design;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service
{
	internal sealed class CompanyDataService: ICompanyDataService
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public CompanyDataService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
		{
			_repository = repositoryManager;
			_logger = logger;
			_mapper = mapper;
		}

        public async Task<CompanyDataDto> CreateCompanyDataAsync(CompanyDataCreationDto companyData, Guid companyId)
        {
            var companyDataEntity = _mapper.Map<CompanyData>(companyData);
            companyDataEntity.CompanyId = companyId;

            var companyDataDb = await _repository.CompanyData.GetCompanyDataByIdAsync(companyId, trackChanges: false);

            if(companyDataDb is null)
            {
                _repository.CompanyData.CreateCompanyData(companyDataEntity);
                await _repository.SaveAsync();

                var companyDataToReturn = _mapper.Map<CompanyDataDto>(companyDataEntity);

                return companyDataToReturn;
            }
            else
            {
                throw new CompanyDataAlreadExists(companyId);
            }


        }

        public async Task DeleteCompanyDataAsync(Guid companyId, bool trackChanges)
        {
            var companyData = await _repository.CompanyData.GetCompanyDataByIdAsync(companyId, trackChanges);

            if (companyData is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            _repository.CompanyData.DeleteCompanyData(companyData);
            await _repository.SaveAsync();

        }

        public async Task<CompanyDataDto> GetCompanyDataAsync(Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyAndChekIfItExists(companyId, trackChanges);

           var companyDataEntity =  await _repository.CompanyData.GetCompanyDataByIdAsync(companyId, trackChanges);

            var companyDataDto = _mapper.Map<CompanyDataDto>(companyDataEntity);

            return companyDataDto;
        }

        public async Task UpdateCompanyDataAsync(Guid companyId, CompanyDataUpdateDto companyDataForUpdate, bool trackChanges)
        {
            var companyDataEntity = await _repository.CompanyData.GetCompanyDataByIdAsync(companyId, trackChanges);

            _mapper.Map(companyDataForUpdate, companyDataEntity);
            await _repository.SaveAsync();
        }

        private async Task<Company> GetCompanyAndChekIfItExists(Guid id, bool trackchanges)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackchanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(id);
            }
            return company;
        }

    }
}

