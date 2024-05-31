using System;
using AcheiProApi.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared;
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


        [HttpPost("passwordUpdate")]
        public async Task<IActionResult> PasswordUpdate([FromBody] PasswordRecoveryDto passwordUpdate, Guid requestId)
        {
           var result =  await _service.PasswordRecoveryService.UpdatePassword(passwordUpdate, requestId);
           if(!result)
           {
                return BadRequest();
           }

         await _service.PasswordRecoveryService.DeleteRequest(requestId);
           
           return Ok();
        }

        [HttpPost("passwordUpdateRequest")]
        public async Task<IActionResult> PasswordUpdateRequest([FromBody] PasswordRecoveryDto passwordUpdate)
        {
           var result =  await _service.PasswordRecoveryService.GeneratePasswordRequest(passwordUpdate);
           if(result != null)
           {    
                await _service.SendEmailService.SendRecoveryPasswordEmail(passwordUpdate, result.Id);
                return Ok();
           }
            return NoContent();
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

        [HttpDelete()]
        [Authorize]
        public async Task<IActionResult> DeleteUser(TokenDto tokenDto, string userName)
        {

            var user = await _service.CompanyService.GetUserFromCompanyId(userName);

           if(await _service.AuthenticationService.DeleteUser(tokenDto))
            {
                if (user.CompanyId != null)
                {
                    await _service.CompanyService.DeleteCompanyAsync((Guid)user.CompanyId, trackchanges: false);
                    await _service.CompanyDataService.DeleteCompanyDataAsync((Guid)user.CompanyId, trackChanges: false);
                }

                return Ok();
            }

            return Unauthorized();
        }

            

        }

    }




