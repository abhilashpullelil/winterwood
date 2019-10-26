using System;
using System.Collections.Generic;

namespace Winterwood.Inventory.Entity
{
    public partial class TrFruit
    {
        public TrFruit()
        {
            TcBatch = new HashSet<TcBatch>();
            TcStock = new HashSet<TcStock>();
        }

        public int FruitId { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<TcBatch> TcBatch { get; set; }
        public virtual ICollection<TcStock> TcStock { get; set; }
    }
}
