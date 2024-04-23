using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class ClientMessage
	{
		[Column("ClientMessageId")]
		public Guid Id { get; set; }
        [Column("companyId")]
        public Guid CompanyId { get; set;}
		public string? Topic { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Interest { get; set; }
        public string? RecipientCompanyName { get; set; }
        public string? RecipientCompanyEmail { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

    }
}

