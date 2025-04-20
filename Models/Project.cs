using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("Projects", Schema = "pm")]
[Index("OwnerId", Name = "IX_pm_Projects_OwnerId")]
public partial class Project
{
    [Key]
    public Guid ProjectId { get; set; }

    public Guid OwnerId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Budget { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [Precision(3)]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Projects")]
    public virtual User Owner { get; set; } = null!;

    [InverseProperty("Project")]
    public virtual ICollection<ProjectFile> ProjectFiles { get; set; } = new List<ProjectFile>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
