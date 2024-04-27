using System;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Text.Json;
using AcheiProApi.Presentation.ActionFilters;
using AcheiProApi.Presentation.ModelBinders;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AcheiProApi.Presentation.Controllers
{
	[Route("api/companies")]
	[ApiController]
	[ResponseCache(CacheProfileName ="120SecondsDuration")]
	public class CompaniesController : ControllerBase
	{
		private readonly IServiceManager _service;

		public CompaniesController(IServiceManager service) => _service = service;

		[HttpDelete("{id:guid}")]
        [Authorize]
		public async Task<IActionResult> DeleteCompany(Guid id)
		{
			await _service.CompanyService.DeleteCompanyAsync(id, trackchanges: false);
            await _service.CompanyDataService.DeleteCompanyDataAsync(id, trackChanges: false);
            return NoContent();
		}


        [HttpDelete("data/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompanyData(Guid id)
        {
            await _service.CompanyDataService.DeleteCompanyDataAsync(id, trackChanges: false);
            return NoContent();
        }

        /// <summary>
        /// Gets the list of all companies
        /// </summary>
        /// <returns>The companies list</returns>
        [HttpGet(Name = "GetCompanies")]
		public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
		{
            
            var pagedResult =  await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false, companyParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.companies);


		}

		[HttpGet("collection/({ids})", Name ="CompanyCollection")]
		public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
		{
			var companies = await _service.CompanyService.GetByIdsAsync(ids, trackChanges: false);

			return Ok(companies);
		}


		[HttpGet("{id:guid}", Name ="CompanyById")]
		public async Task<IActionResult> GetCompany(Guid id)
		{
			var company = await _service.CompanyService.GetCompanyAsync(companyId: id, trackChanges: false);
			return Ok(company);
		}


        [HttpGet("data/{id:guid}", Name = "GetCompanyData")]
        public async Task<IActionResult> GetDataCompany(Guid id)
        {
            var company = await _service.CompanyDataService.GetCompanyDataAsync(companyId: id, trackChanges: false);

            if(company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        /// <summary>
        /// Creates a new company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>A newly created company</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="422">If the model is invalid</response>
        /// 
        [HttpPost  (Name ="CreateCompany")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto company)
		{

            var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);

			return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
		}


        [HttpPost("data", Name ="CreateDataCompany")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> CreateDataCompany([FromBody] CompanyDataCreationDto companyData, Guid companyId)
        {

            var createdCompanyData = await _service.CompanyDataService.CreateCompanyDataAsync(companyData, companyId);

            return CreatedAtRoute("GetCompanyData", new { id = createdCompanyData.Id }, createdCompanyData);
        } 


        [HttpPost("image")]
        //[Authorize]
        public async Task<IActionResult> CompanyAddImage([FromForm] CompanyImageDto model)
        {

            if (model.ImageFile != null)
            {
                var fileResult =  _service.CompanyImageService.SaveImageToResources(model.ImageFile);

                if (fileResult.Result.Item1 == 0)
                {
                    return BadRequest("Only .jpg, .png, .jpeg extensions are allowed");
                }

                if (fileResult.Result.Item1 == 1)
                 {
                     model.ImageName = fileResult.Result.Item2; // getting name of image

					 await _service.CompanyImageService.AddImageModel(model.CompanyId, model, trackChanges: false);

                    var imageUri = await _service.CompanyImageService.GetImageName(model.CompanyId, trackChanges: false);

                    return Ok(imageUri);
                }

            }

            return BadRequest("Please insert a valid image file");

        }

        [HttpPut("image")]
        //[Authorize]
        public async Task<IActionResult> UpdateAddImage([FromForm] CompanyImageDto model)
        {

            var company = _service.CompanyImageService.CheckIfCompanyExists(model.CompanyId, false);

            if (model.ImageFile != null)
            {
                var fileResult = await  _service.CompanyImageService.SaveImageToResources(model.ImageFile);

                if (fileResult.Item1 ==0)
                {
                    return BadRequest("Only .jpg, .png, .jpeg extensions are allowed");
                }
                else
                {
                    model.ImageName = fileResult.Item2;
                    await _service.CompanyImageService.UpdateImage(model, trackChanges: true);
                }

                var imageUri = await _service.CompanyImageService.GetImageName(model.CompanyId, trackChanges: false);

               
                return Ok(imageUri);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("imageName")]
		public async Task<IActionResult> GetImageUri(Guid companyId)
		{
			var imageUri = await _service.CompanyImageService.GetImageName(companyId, trackChanges: false);
			return Ok(imageUri);
		} 


        [HttpPost("collection")]
        [Authorize]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyCreationDto> companyCollection)
		{
			var result = await _service.CompanyService.CreateCompanyCollectionAsync(companyCollection);

			return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
		}

		[HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
		{
			await _service.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);
			return NoContent();
		}

        [HttpPut("data/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> UpdateCompanyData(Guid id, [FromBody] CompanyDataUpdateDto companyDataUpdate)
        {
            await _service.CompanyDataService.UpdateCompanyDataAsync(id, companyDataUpdate, trackChanges: true);
            return NoContent();
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> UserCompanyId(string userName)
        {

            var user = await _service.CompanyService.GetUserFromCompanyId(userName);

            if (user == null)
            {
                return NotFound();
            }


            return Ok(user);
        }

        [HttpPut("updateUserCompanyId")]
        [Authorize]
        public async Task<IActionResult> UpdateUserCompanyId([FromBody] UserDto user)
        {
            UserDto result;

            if(user.CompanyId == null)
            {
                result = await _service.CompanyService.UpdateUserCompanyId(user.UserName, System.Guid.Empty);
            }
            else
            {
                result = await _service.CompanyService.UpdateUserCompanyId(user.UserName, (Guid)user.CompanyId);
            }

             
            if(result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }


    }
}

