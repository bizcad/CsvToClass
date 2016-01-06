using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvClassGenerator
{
    public class PropertySpecification
    {
        public int Id { get; set; }
        public CTEnum ColumnType { get; set; }
        public string ColumnName { get; set; }
        public bool Nullable { get; set; }
    }
}
