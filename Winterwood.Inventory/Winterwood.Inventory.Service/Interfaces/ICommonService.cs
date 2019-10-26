using System;
using System.Collections.Generic;
using System.Text;
using Winterwood.Inventory.DTO;

namespace Winterwood.Inventory.Service.Interfaces
{
    public interface ICommonService
    {
        List<FruitDTO> GetAllFruits();

        List<VarietyDTO> GetAllVariety();
    }
}
