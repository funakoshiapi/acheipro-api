﻿using System;
using AcheiProApi.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace AcheiProApi.Presentation.Controllers
{
	[Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
	{
		private readonly IServiceManager _service;

		public AuthenticationController(IServiceManager service) => _service = service;

		[HttpPost]
		[ServiceFilter (typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
		{
			var result = await _service.AuthenticationService.RegisterUser(userForRegistration);

			if(!result.Succeeded)
			{
				foreach(var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}

				return BadRequest(ModelState);
			}

			return StatusCode(201);
		}


        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
            {
                return Unauthorized();
            }

			var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);

			return Ok(tokenDto);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(TokenDto tokenDto, Guid id)
        {
           if( await _service.AuthenticationService.DeleteUser(tokenDto))
            {
                await _service.CompanyService.DeleteCompanyAsync(id, trackchanges: false);
                await _service.CompanyDataService.DeleteCompanyDataAsync(id, trackChanges: false);
                return NoContent();
            }

            return Unauthorized();



        }

    }


}

