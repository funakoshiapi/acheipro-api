﻿using System.Text.Json;
using AcheiProApi.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace AcheiProApi.Presentation.Controllers
{
	[Route("api/companies/{companyId}/employees")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly IServiceManager _service;
		public EmployeesController(IServiceManager service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEmployees(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
		{
		
            var pagedResult = await _service.EmployeeService.GetAllEmployeesAsync(companyId, employeeParameters, trackChanges: false);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

			return Ok(pagedResult.employees);

		}

		[HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
		public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
		{
			var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);

			return Ok(employee);
		}

		[HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeCreationDto employee)
		{
			if(employee is null)
			{
				return BadRequest("EmployeeCreationDto object is null");
			}

			var employeeToReturn = await  _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges: false);

			return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
		{
			await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackchanges: false);

			return NoContent();
		}

		[HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
		{
			await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee, compTrackChanges: false, empTrackChanges: true);

			return NoContent();
		}


        [HttpPatch("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
    [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id,
                compTrackChanges: false, empTrackChanges: true);

            patchDoc.ApplyTo(result.employeeToPatch);

            TryValidateModel(result.employeeToPatch);

            /*if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);*/

            await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }
    }
}

