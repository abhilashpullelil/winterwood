using System;
using System.Collections.Generic;

namespace Winterwood.Inventory.Entity
{
    public partial class TrVariety
    {
        public TrVariety()
        {
            TcBatch = new HashSet<TcBatch>();
            TcStock = new HashSet<TcStock>();
        }

        public int VarietyId { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<TcBatch> TcBatch { get; set; }
        public virtual ICollection<TcStock> TcStock { get; set; }
    }
}
