
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace AcheiProApi.Presentation.Controllers
{
	[Route("api/email")]
	[ApiController]
	public class EmailsController : ControllerBase
	{
		private readonly IServiceManager _service;
		public EmailsController(IServiceManager service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("sendemail")]
        public async Task<IActionResult> TestEmail([FromBody] ClientMessageDto clientMessage)
        {
            try
            {
                await _service.SendEmailService.SendClientMessageAsync(clientMessage);
             
              return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Email failed to send");
            }
        }

    }
}

