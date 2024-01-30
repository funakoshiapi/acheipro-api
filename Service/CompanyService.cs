using System;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class CompanyService : ICompanyService

	{
        private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company =  await GetCompanyAndChekIfItExists(companyId, trackChanges);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }

        public async Task <CompanyDto> CreateCompanyAsync(CompanyCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyCreationDto> companyCollection)
        {
            if(companyCollection is null)
            {
                throw new CompanyCollectionBadRequest();
            }

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }

            await _repository.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return (companies: companyCollectionToReturn, ids: ids);
        }

        public async Task<(IEnumerable<CompanyDto> companies, MetaData metaData )> GetAllCompaniesAsync(bool trackChanges, CompanyParameters companyParameters )
        {
        
            var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges, companyParameters);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return (companies: companiesDto, metaData: companies.MetaData);

        }

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if( ids == null)
            {
                throw new IdParametersBadRequestException();
            }

            var companyEntities = await _repository.Company.GetByIdsAsync(ids, trackChanges);

            if( ids.Count() != companyEntities.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            return companiesToReturn;
        }

        public async Task<CompanyDto> GetCompanyAsync (Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyAndChekIfItExists(companyId, trackChanges);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;

        }

        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
        {
            var companyEntity = await GetCompanyAndChekIfItExists(companyId, trackChanges);

            _mapper.Map(companyForUpdate, companyEntity);
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

