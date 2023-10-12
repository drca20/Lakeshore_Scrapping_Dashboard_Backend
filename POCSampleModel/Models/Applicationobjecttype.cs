using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("applicationobjecttype")]
public partial class Applicationobjecttype
{
    [Key]
    [Column("ApplicationObjectTypeID")]
    public int ApplicationObjectTypeId { get; set; }

    [StringLength(50)]
    public string ApplicationObjectTypeName { get; set; } = null!;
}
