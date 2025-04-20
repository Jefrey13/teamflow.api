using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("ProjectFiles", Schema = "pm")]
public partial class ProjectFile
{
    [Key]
    public Guid FileId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid UploadedBy { get; set; }

    [StringLength(255)]
    public string FileName { get; set; } = null!;

    [StringLength(50)]
    public string FileType { get; set; } = null!;

    public long FileSize { get; set; }

    [StringLength(2000)]
    public string Url { get; set; } = null!;

    [Precision(3)]
    public DateTime UploadedAt { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectFiles")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("UploadedBy")]
    [InverseProperty("ProjectFiles")]
    public virtual User UploadedByNavigation { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    public DateTime? RemovedAt { get; set; }

}
