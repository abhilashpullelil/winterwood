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
    public class CommonService: ICommonService
    {
        public List<FruitDTO> GetAllFruits()
        {
            using(var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TrFruit>(unitOfWork);
                var fruits = repo.Find(x=>x.IsDelete == false).ToList();
                if (fruits == null)
                    return new List<FruitDTO>();
                return fruits.Select(x => new FruitDTO() { 
                    FruitId = x.FruitId,
                    Name = x.Name
                }).OrderBy(x=>x.Name)
                .ToList();
            }
        }

        public List<VarietyDTO> GetAllVariety()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repo = new Repository<TrVariety>(unitOfWork);
                var fruits = repo.Find(x => x.IsDelete == false).ToList();
                if (fruits == null)
                    return new List<VarietyDTO>();
                return fruits.Select(x => new VarietyDTO()
                {
                    VarietyId = x.VarietyId,
                    Name = x.Name
                }).OrderBy(x => x.Name)
                .ToList();
            }
        }
    }
}
