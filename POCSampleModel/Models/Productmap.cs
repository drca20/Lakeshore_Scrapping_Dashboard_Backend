using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("productmap")]
public partial class Productmap
{
    [Key]
    public int ProductMapId { get; set; }

    public int LakeshoreProductId { get; set; }

    public int CompititorsProductId { get; set; }

    public int? Type { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDateTime { get; set; }

    public int? Status { get; set; }
}
