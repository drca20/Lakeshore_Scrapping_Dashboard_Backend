using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("lakeshoreproduct")]
public partial class Lakeshoreproduct
{
    [Key]
    [Column("lakeshoreproductid")]
    public int Lakeshoreproductid { get; set; }

    [Column("LKSproductname")]
    [StringLength(50)]
    public string? Lksproductname { get; set; }

    [Column("LKSSKU")]
    [StringLength(50)]
    public string? Lkssku { get; set; }

    [Column("LKSURL")]
    [StringLength(500)]
    public string? Lksurl { get; set; }

    [Column("LKSPrice")]
    [StringLength(50)]
    public string? Lksprice { get; set; }

    [StringLength(8000)]
    public string? ImageUrl { get; set; }

    public int? Status { get; set; }
}
