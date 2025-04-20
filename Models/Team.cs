using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("Teams", Schema = "pm")]
public partial class Team
{
    [Key]
    public Guid TeamId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [Precision(3)]
    public DateTime UpdatedAt { get; set; }


    public bool IsActive { get; set; } = true;
    public DateTime? RemovedAt { get; set; }
}
