using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("compititorproduct")]
public partial class Compititorproduct
{
    [Key]
    [Column("CompititorProductID")]
    public int CompititorProductId { get; set; }

    [StringLength(100)]
    public string? Compititorproductname { get; set; }

    [Column("CompititorSKU")]
    [StringLength(50)]
    public string? CompititorSku { get; set; }

    [Column("CompititorURL")]
    [StringLength(500)]
    public string? CompititorUrl { get; set; }

    [StringLength(45)]
    public string? CompititorPrice { get; set; }

    [StringLength(8000)]
    public string? ImageUrl { get; set; }
}
