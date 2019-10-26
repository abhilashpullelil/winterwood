using System;
using System.Collections.Generic;
using System.Text;

namespace Winterwood.Inventory.DTO
{
    public class BatchHomeVM
    {
        public List<BatchDTO> BatchList { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<FruitDTO> AvailableFruits { get; set; }
        public List<VarietyDTO> AvailableVariety { get; set; }

        public bool HasPreviousPage { 
            get
            {
                if (this.CurrentPage <= 1)
                    return false;
                return true;
            }
        }
        public bool HasNextPage {
            get
            {
                var currentItem = (PageSize * (this.CurrentPage - 1)) + this.PageSize;
                if (this.TotalCount > currentItem)
                    return true;
                return false;
            }
        }
    }
}
