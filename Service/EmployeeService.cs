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
	internal sealed class EmployeeService : IEmployeeService
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
		{
			_repositoryManager = repository;
			_logger = logger;
			_mapper = mapper;
		}

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
			await CheckIfCompanyExists(companyId, compTrackChanges);

			var employeeDb = await GetEmployeeFromCompanyAndCheckIfExists(companyId, id, empTrackChanges);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeDb);

            return (employeeToPatch, employeeEntity: employeeDb);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee
		employeeEntity)
		{
			_mapper.Map(employeeToPatch, employeeEntity);
            await _repositoryManager.SaveAsync();
		}

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeCreationDto employeeCreation, bool trackChanges)
        {

			await CheckIfCompanyExists(companyId, trackChanges);

			var employeeEntity = _mapper.Map<Employee>(employeeCreation);

			_repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
			await _repositoryManager.SaveAsync();

			var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

			return employeeToReturn;
        }


        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetAllEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
		{
			await CheckIfCompanyExists(companyId, trackChanges);

			var employeesWithMetaData = await _repositoryManager.Employee.GetAllEmployeesAsync(companyId, employeeParameters, trackChanges);
		
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

			return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);
            
		}

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {

			await CheckIfCompanyExists(companyId, trackChanges);

			var employeeDb = await GetEmployeeFromCompanyAndCheckIfExists(companyId, id, trackChanges);

			var employee = _mapper.Map<EmployeeDto>(employeeDb);

			return employee;
				
        }

		public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
		{
            await CheckIfCompanyExists(companyId, trackChanges);

            var employee = await GetEmployeeFromCompanyAndCheckIfExists(companyId, id, trackChanges);

			if(employee is null)
			{
				throw new EmployeeNotFoundException(id);
			}

			_repositoryManager.Employee.DeleteEmployee(employee);
			await _repositoryManager.SaveAsync();
		}

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
			await CheckIfCompanyExists(companyId, compTrackChanges);

			var employee = await GetEmployeeFromCompanyAndCheckIfExists(companyId, id, empTrackChanges);

			_mapper.Map(employeeForUpdate, employee);
			await _repositoryManager.SaveAsync();
        }

		private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
		{
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

        }

		private async Task<Employee> GetEmployeeFromCompanyAndCheckIfExists(Guid companyId, Guid id, bool trackChanges)
		{
            var employeeDb = await _repositoryManager.Employee.GetEmployeeAsync(companyId, id, trackChanges);

            if (employeeDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }

			return employeeDb;
        }
    }
}

