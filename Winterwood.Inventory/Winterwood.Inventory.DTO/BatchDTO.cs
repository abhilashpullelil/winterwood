using System;

namespace Winterwood.Inventory.DTO
{
    public class BatchDTO
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

        public FruitDTO Fruit { get; set; }
        public VarietyDTO Variety { get; set; }

        public string FruitName { get; set; }
        public string VarietyName { get; set; }
    }
}
