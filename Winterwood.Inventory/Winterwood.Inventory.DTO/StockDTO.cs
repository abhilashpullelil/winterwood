using System;
using System.Collections.Generic;
using System.Text;

namespace Winterwood.Inventory.DTO
{
    public class StockDTO
    {
        public int StockId { get; set; }
        public int FruitId { get; set; }
        public int VarietyId { get; set; }
        public int? Quantity { get; set; }

        public FruitDTO Fruit { get; set; }
        public VarietyDTO Variety { get; set; }

        public string FruitName { get; set; }
        public string VarietyName { get; set; }
    }
}
