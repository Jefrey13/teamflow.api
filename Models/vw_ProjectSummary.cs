using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Keyless]
public partial class vw_ProjectSummary
{
    public Guid ProjectId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    [StringLength(20)]
    public string Status { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Budget { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? ProgressPercent { get; set; }

    public int? TotalTasks { get; set; }

    public int? CompletedTasks { get; set; }
}
