using System;
using AcheiProApi.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace AcheiProApi.Presentation.Controllers
{
	[Route("api/token")]
	[ApiController]
	public class TokenContreller : ControllerBase
	{
		private readonly IServiceManager _service;

        public TokenContreller(IServiceManager service)
        {
            _service = service;
        }

		[HttpPost("refresh")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
		{
			var tokenDtoReturn = await _service.AuthenticationService.RefreshToken(tokenDto);

			return Ok(tokenDtoReturn);
		}

 
	}
}

