using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("productpricehistory")]
[Index("ProductId", Name = "fk_producthistory_product_idx")]
public partial class Productpricehistory
{
    [Key]
    public int ProductPriceHistoryId { get; set; }

    public int ProductId { get; set; }

    [Precision(10)]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDateTime { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    public int? Status { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Productpricehistories")]
    public virtual Product Product { get; set; } = null!;
}
