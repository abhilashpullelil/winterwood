using System;
using System.Collections.Generic;
using System.Text;
using Winterwood.Inventory.DTO;

namespace Winterwood.Inventory.Service.Interfaces
{
    public interface IStockService
    {
        List<StockDTO> GetAll(int pageSize, int currentPage);
        void AddStock(BatchDTO model);
        void UpdateStock(BatchDTO model, int oldQuantity);
        int GetTotalCount();
    }
}
