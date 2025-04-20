using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Keyless]
public partial class vw_ProjectTaskDetail
{
    public Guid TaskId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [StringLength(10)]
    public string Priority { get; set; } = null!;

    public DateOnly? DueDate { get; set; }

    public Guid ProjectId { get; set; }

    [StringLength(50)]
    public string? AssignedUser { get; set; }
}
