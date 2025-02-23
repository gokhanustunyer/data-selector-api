using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.DTOs.Table
{
    public class DbTable
    {
        public string Title { get; set; }
        public List<string> Columns { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
    }
}