using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class PasswordRecovery
{
    [Column("RecoveryId")]
    public Guid Id{ get; set; }
    public string? Email { get; set; }
}
