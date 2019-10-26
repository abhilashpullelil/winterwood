using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winterwood.Inventory.DTO;
using Winterwood.Inventory.Entity;
using Winterwood.Inventory.Repository;
using Winterwood.Inventory.Service.Interfaces;

namespace Winterwood.Inventory.Service
{
    public class StockService: IStockService
    {
        public List<StockDTO> GetAll(int pageSize, int currentPage)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcStock>(unitOfWork);
                var list = repo.AsQueryable()
                                .OrderByDescending(x => x.StockId)
                                .Skip(pageSize * (currentPage - 1))
                                .Take(pageSize)
                                .Include(x => x.Variety)
                                .Include(x => x.Fruit)
                                .ToList();
                if (list == null)
                    return new List<StockDTO>();
                return list.Select(x => new StockDTO()
                {
                    StockId = x.StockId,
                    FruitId = x.FruitId,
                    Quantity = x.Quantity,
                    VarietyId = x.VarietyId,
                    FruitName = x.Fruit.Name,
                    VarietyName = x.Variety.Name
                }).ToList();
            }
        }

        public int GetTotalCount()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcStock>(unitOfWork);
                var list = repo.AsQueryable()
                                .OrderByDescending(x => x.StockId)
                                .ToList();
                if (list == null)
                    return 0;
                return list.Count;
            }
        }

        public void AddStock(BatchDTO model)
        {
            using(var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcStock>(unitOfWork);
                var currentStock = repo.FirstOrDefault(x => x.FruitId == model.FruitId && x.VarietyId == model.VarietyId);
                if(currentStock == null)
                {
                    // There is no stock for this product. Then create a stock for this
                    repo.Add(new TcStock() { 
                        FruitId = model.FruitId,
                        VarietyId = model.VarietyId,
                        Quantity = model.Quantity
                    });
                    return;
                }
                currentStock.Quantity += model.Quantity; // Increasing the stock quantity
                unitOfWork.SaveChanges();
                return;
            }
        }

        public void UpdateStock(BatchDTO model, int oldQuantity)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TcStock>(unitOfWork);
                var currentStock = repo.FirstOrDefault(x => x.FruitId == model.FruitId && x.VarietyId == model.VarietyId);
                if (currentStock == null)
                {
                    // It's an exception, throw an error
                    throw new Exception("Cann't find the stock");
                }
                currentStock.Quantity -= oldQuantity;
                currentStock.Quantity += model.Quantity;
                unitOfWork.SaveChanges();
                return;
            }
        }
    }
}
