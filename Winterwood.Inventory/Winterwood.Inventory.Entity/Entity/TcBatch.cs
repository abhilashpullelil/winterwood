using System;
using System.Collections.Generic;

namespace Winterwood.Inventory.Entity
{
    public partial class TcBatch
    {
        public int BatchId { get; set; }
        public int FruitId { get; set; }
        public int VarietyId { get; set; }
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }

        public virtual TrFruit Fruit { get; set; }
        public virtual TrVariety Variety { get; set; }
    }
}
