using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("compititors")]
public partial class Compititor
{
    [Key]
    public int CompititorsId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Url { get; set; }

    [InverseProperty("Compititor")]
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
