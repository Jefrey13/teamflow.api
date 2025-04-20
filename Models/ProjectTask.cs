using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("ProjectTasks", Schema = "pm")]
[Index("ProjectId", "Status", Name = "IX_pm_ProjectTasks_Project_Status")]
public partial class ProjectTask
{
    [Key]
    public Guid TaskId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid? AssignedTo { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DueDate { get; set; }

    [StringLength(10)]
    public string Priority { get; set; } = null!;

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [Precision(3)]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("AssignedTo")]
    [InverseProperty("ProjectTasks")]
    public virtual User? AssignedToNavigation { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectTasks")]
    public virtual Project Project { get; set; } = null!;
}
