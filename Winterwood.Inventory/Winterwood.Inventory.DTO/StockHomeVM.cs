using System;
using System.Collections.Generic;
using System.Text;

namespace Winterwood.Inventory.DTO
{
    public class StockHomeVM
    {
        public List<StockDTO> StockList { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<FruitDTO> AvailableFruits { get; set; }
        public List<VarietyDTO> AvailableVariety { get; set; }
    }
}
