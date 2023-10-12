using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("compititorproductmap")]
public partial class Compititorproductmap
{
    [Key]
    [Column("compititorproductmapID")]
    public int CompititorproductmapId { get; set; }

    [Column("lakeshoreprouctID")]
    public int? LakeshoreprouctId { get; set; }

    [Column("compititorproductID")]
    public int? CompititorproductId { get; set; }

    [Column("matchtype")]
    public int? Matchtype { get; set; }

    public int? Status { get; set; }
}
