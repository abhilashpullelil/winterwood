using System;
using System.Collections.Generic;
using System.Text;
using Winterwood.Inventory.DTO;

namespace Winterwood.Inventory.Service.Interfaces
{
    public interface ICommonService
    {
        /// <summary>
        /// Get all fruits
        /// </summary>
        /// <returns></returns>
        List<FruitDTO> GetAllFruits();

        /// <summary>
        /// Get all varieties
        /// </summary>
        /// <returns></returns>
        List<VarietyDTO> GetAllVariety();
    }
}
