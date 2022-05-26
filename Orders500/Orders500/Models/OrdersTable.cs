using System;
using System.Collections.Generic;

namespace Orders500.Models
{
    public partial class OrdersTable
    {
        public int OrdersId { get; set; }
        public int StoreId { get; set; }
        public int SalesPersonId { get; set; }
        public int CdId { get; set; }
        public int PricePaid { get; set; }
        public string Date { get; set; } = null!;

        public virtual CdTable Cd { get; set; } = null!;
        public virtual SalesPersonTable SalesPerson { get; set; } = null!;
        public virtual StoreTable Store { get; set; } = null!;
    }
}
