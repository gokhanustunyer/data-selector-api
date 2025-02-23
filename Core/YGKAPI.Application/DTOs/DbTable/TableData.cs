using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.DTOs.DbTable
{
    public class TableData
    {
        public List<string> Columns { get; set; }
        public List<TableRow> Rows { get; set; }
    }
}