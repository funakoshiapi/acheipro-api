using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class PasswordRecovery
{
    [Column("RecoveryId")]
    public required Guid Id{ get; set; }
    public required Guid CompanyId { get; set; }
    public required string UserName { get; set; }
}
