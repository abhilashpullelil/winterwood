using System;
using System.Collections.Generic;
using System.Text;
using Winterwood.Inventory.DTO;

namespace Winterwood.Inventory.Service.Interfaces
{
    public interface IBatchService
    {
        void AddBatch(BatchDTO model);
        List<BatchDTO> GetAll(int pageSize, int currentPage);
        int GetTotalCount();
        BatchDTO DeleteBatch(int batchId);
        BatchDTO GetDetails(int batchId);
        int UpdateQuantity(BatchDTO model);
    }
}
