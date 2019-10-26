using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using Winterwood.Inventory.DTO;
using Winterwood.Inventory.Entity;
using Winterwood.Inventory.Repository;
using Winterwood.Inventory.Service.Interfaces;

namespace Winterwood.Inventory.Service
{
    public class BatchService : IBatchService
    {
        public void AddBatch(BatchDTO model)
        {
            using(var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcBatch>(unitOfWork);
                repo.Add(new TcBatch() { 
                    CreatedBy = model.CreatedBy,
                    CreatedDateUtc = DateTime.UtcNow,
                    FruitId = model.FruitId,
                    IsDelete = false,
                    Quantity = model.Quantity,
                    VarietyId = model.VarietyId
                });
            }
        }

        public List<BatchDTO> GetAll(int pageSize, int currentPage)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcBatch>(unitOfWork);
                var list = repo.AsQueryable().Where(x => x.IsDelete == false)
                                .OrderByDescending(x => x.CreatedDateUtc)
                                .Skip(pageSize * (currentPage - 1))
                                .Take(pageSize)
                                .Include(x=>x.Variety)
                                .Include(x=>x.Fruit)
                                .ToList();
                if (list == null)
                    return new List<BatchDTO>();
                return list.Select(x => new BatchDTO() { 
                    BatchId = x.BatchId,
                    CreatedDateUtc = x.CreatedDateUtc,
                    FruitId = x.FruitId,
                    Quantity = x.Quantity,
                    VarietyId = x.VarietyId,
                    FruitName = x.Fruit.Name,
                    VarietyName = x.Variety.Name
                }).ToList();
            }
        }

        public BatchDTO GetDetails(int batchId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcBatch>(unitOfWork);
                var batch = repo.AsQueryable()
                                .Include(x => x.Variety)
                                .Include(x => x.Fruit)
                                .FirstOrDefault(x => x.BatchId == batchId && x.IsDelete == false);
                if (batch == null)
                    throw new NullReferenceException();
                return new BatchDTO()
                {
                    BatchId = batch.BatchId,
                    CreatedDateUtc = batch.CreatedDateUtc,
                    FruitId = batch.FruitId,
                    Quantity = batch.Quantity,
                    VarietyId = batch.VarietyId,
                    FruitName = batch.Fruit.Name,
                    VarietyName = batch.Variety.Name
                };
            }
        }

        public int UpdateQuantity(BatchDTO model)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcBatch>(unitOfWork);
                var batch = repo.FirstOrDefault(x => x.BatchId == model.BatchId && x.IsDelete == false);
                if (batch == null)
                    throw new NullReferenceException();
                var oldQuantity = batch.Quantity;
                batch.Quantity = model.Quantity;
                batch.UpdatedBy = model.UpdatedBy;
                batch.UpdatedDateUtc = DateTime.UtcNow;
                unitOfWork.SaveChanges();
                return oldQuantity;
            }
        }

        public int GetTotalCount()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcBatch>(unitOfWork);
                var count = repo.AsQueryable().Where(x => x.IsDelete == false)
                                .Count();
                return count;
            }
        }

        public BatchDTO DeleteBatch(int batchId)
        {
            using(var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcBatch>(unitOfWork);
                var entity = repo.FirstOrDefault(x => x.BatchId == batchId);
                if(entity != null)
                {
                    entity.IsDelete = true; // Soft delete
                    unitOfWork.SaveChanges();
                    return new BatchDTO() { 
                        BatchId = entity.BatchId,
                        FruitId = entity.FruitId,
                        VarietyId = entity.VarietyId,
                        Quantity = entity.Quantity
                    };
                }
                throw new Exception("Cann't find the batch with batch ID "+ batchId);
            }
        }
    }
}
