using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("products")]
[Index("CompititorId", Name = "CompititorId_idx")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    public int? CompititorId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    public string? Sku { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? Category { get; set; }

    [Precision(10)]
    public decimal? CurrentPrice { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDateTime { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public int? Status { get; set; }

    [ForeignKey("CompititorId")]
    [InverseProperty("Products")]
    public virtual Compititor? Compititor { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Productpricehistory> Productpricehistories { get; } = new List<Productpricehistory>();
}
