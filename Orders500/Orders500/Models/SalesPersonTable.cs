using System;
using System.Collections.Generic;

namespace Orders500.Models
{
    public partial class SalesPersonTable
    {
        public SalesPersonTable()
        {
            OrdersTables = new HashSet<OrdersTable>();
        }

        public int SalesPersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public int StoreId { get; set; }

        public virtual StoreTable Store { get; set; } = null!;
        public virtual ICollection<OrdersTable> OrdersTables { get; set; }
    }
}
