using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("Roles", Schema = "auth")]
[Index("RoleName", Name = "UQ__Roles__8A2B616036DB26C0", IsUnique = true)]
public partial class Role
{
    [Key]
    public byte RoleId { get; set; }

    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
