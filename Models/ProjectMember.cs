using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[PrimaryKey("ProjectId", "UserId")]
[Table("ProjectMembers", Schema = "pm")]
public partial class ProjectMember
{
    [Key]
    public Guid ProjectId { get; set; }

    [Key]
    public Guid UserId { get; set; }

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [Precision(3)]
    public DateTime AddedAt { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectMembers")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ProjectMembers")]
    public virtual User User { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    public DateTime? RemovedAt { get; set; }
}
