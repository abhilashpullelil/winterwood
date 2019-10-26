using System;
using System.Collections.Generic;
using System.Text;
using Winterwood.Inventory.DTO;

namespace Winterwood.Inventory.Service.Interfaces
{
    public interface IBatchService
    {
        /// <summary>
        /// Add new batch 
        /// </summary>
        /// <param name="model">DTO to be added</param>
        void AddBatch(BatchDTO model);
        /// <summary>
        /// Get all batches in a paginated format
        /// </summary>
        /// <param name="pageSize"> items per page</param>
        /// <param name="currentPage"> current page. Starting from 1 to n </param>
        /// <returns></returns>
        List<BatchDTO> GetAll(int pageSize, int currentPage);
        /// <summary>
        /// Get total number of batches
        /// </summary>
        /// <returns></returns>
        int GetTotalCount();
        /// <summary>
        /// Soft delete a batch
        /// </summary>
        /// <param name="batchId">Id of the batch to be deleted</param>
        /// <returns></returns>
        BatchDTO DeleteBatch(int batchId);
        /// <summary>
        /// Get details of a batch
        /// </summary>
        /// <param name="batchId">Id of the batch</param>
        /// <returns></returns>
        BatchDTO GetDetails(int batchId);
        /// <summary>
        /// Update the quantity of the batch
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateQuantity(BatchDTO model);
    }
}
