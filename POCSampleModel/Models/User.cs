using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("user")]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(10000)]
    public string? Password { get; set; }

    [StringLength(1000)]
    public string? Profile { get; set; }

    [StringLength(60)]
    public string? RefreshToken { get; set; }

    public int? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RefreshTokenExpiry { get; set; }

    public int? LastLogin { get; set; }
}
