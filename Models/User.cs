using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace teamflow.API.Models;

[Table("Users", Schema = "auth")]
[Index("Username", Name = "UQ__Users__536C85E4352D05DE", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D10534A3BB998B", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid UserId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(256)]
    public string PasswordHash { get; set; } = null!;

    public bool IsActive { get; set; }

    [Precision(3)]
    public DateTime CreatedAt { get; set; }

    [Precision(3)]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("UploadedByNavigation")]
    public virtual ICollection<ProjectFile> ProjectFiles { get; set; } = new List<ProjectFile>();

    [InverseProperty("User")]
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    [InverseProperty("AssignedToNavigation")]
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    [InverseProperty("Owner")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
