using System;
using System.Collections.Generic;

namespace Orders500.Models
{
    public partial class CdTable
    {
        public CdTable()
        {
            OrdersTables = new HashSet<OrdersTable>();
        }

        public int CdId { get; set; }
        public string Cdname { get; set; } = null!;
        public string Artist { get; set; } = null!;
        public int YearReleased { get; set; }
        public int ListPrice { get; set; }

        public virtual ICollection<OrdersTable> OrdersTables { get; set; }
    }
}
