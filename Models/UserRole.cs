using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[PrimaryKey("UserId", "RoleId")]
[Table("UserRoles", Schema = "auth")]
public partial class UserRole
{
    [Key]
    public Guid UserId { get; set; }

    [Key]
    public byte RoleId { get; set; }

    [Precision(3)]
    public DateTime AssignedAt { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoles")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User User { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    public DateTime? RemovedAt { get; set; }
}
