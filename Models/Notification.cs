using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("Notifications", Schema = "pm")]
public partial class Notification
{
    [Key]
    public Guid NotificationId { get; set; }

    public Guid UserId { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? TaskId { get; set; }

    [StringLength(500)]
    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("Notifications")]
    public virtual Project? Project { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("Notifications")]
    public virtual ProjectTask? Task { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Notifications")]
    public virtual User User { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
