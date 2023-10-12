using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

[Table("applicationobject")]
public partial class Applicationobject
{
    [Key]
    [Column("ApplicationObjectID")]
    public int ApplicationObjectId { get; set; }

    [StringLength(50)]
    public string ApplicationObjectName { get; set; } = null!;

    [Column("ApplicationObjectTypeID")]
    public int ApplicationObjectTypeId { get; set; }
}
