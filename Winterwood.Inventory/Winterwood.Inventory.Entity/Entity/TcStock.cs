using System;
using System.Collections.Generic;

namespace Winterwood.Inventory.Entity
{
    public partial class TcStock
    {
        public int StockId { get; set; }
        public int FruitId { get; set; }
        public int VarietyId { get; set; }
        public int? Quantity { get; set; }

        public virtual TrFruit Fruit { get; set; }
        public virtual TrVariety Variety { get; set; }
    }
}
