using System;
using System.Collections.Generic;
using System.Text;
using Winterwood.Inventory.DTO;

namespace Winterwood.Inventory.Service.Interfaces
{
    public interface IStockService
    {
        /// <summary>
        /// Get all stocks in paginated format
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        List<StockDTO> GetAll(int pageSize, int currentPage);
        /// <summary>
        /// Add a stock
        /// </summary>
        /// <param name="model"></param>
        void AddStock(BatchDTO model);
        /// <summary>
        /// Update the stock
        /// </summary>
        /// <param name="model"></param>
        /// <param name="oldQuantity"></param>
        void UpdateStock(BatchDTO model, int oldQuantity);
        /// <summary>
        /// Get total number of stocks
        /// </summary>
        /// <returns></returns>
        int GetTotalCount();
    }
}
