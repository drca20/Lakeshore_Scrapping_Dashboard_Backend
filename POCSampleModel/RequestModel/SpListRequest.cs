using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.RequestModel
{
    public class SpListRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public string Filters { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
