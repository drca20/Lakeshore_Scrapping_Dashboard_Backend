using Microsoft.EntityFrameworkCore.Migrations.Operations;
using POCSampleModel.SpDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.ResponseModel
{
    public  class ddlProductByCompititorID 
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
    }
}
