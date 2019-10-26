using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Winterwood.Inventory.DTO;
using Winterwood.Inventory.Service.Interfaces;
using Winterwood.Inventory.Web.Models;

namespace Winterwood.Inventory.Web.Controllers
{
    /// <summary>
    /// Restricted resource. Only authorized users can access
    /// </summary>
    [Authorize]
    public class StockController : Controller
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stock;

        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            this._logger = logger;
            this._stock = stockService;
        }

        /// <summary>
        /// List all stocks
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var model = new StockHomeVM();
            model.PageSize = 10;
            model.CurrentPage = 1;
            model.StockList = this._stock.GetAll(model.PageSize, model.CurrentPage);
            model.TotalCount = this._stock.GetTotalCount();
            return View(model);
        }
    }
}
