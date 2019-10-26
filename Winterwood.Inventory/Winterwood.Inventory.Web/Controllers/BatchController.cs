using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Winterwood.Inventory.DTO;
using Winterwood.Inventory.Service.Interfaces;
using Winterwood.Inventory.Web.Models;

namespace Winterwood.Inventory.Web.Controllers
{
    [Authorize]
    public class BatchController : Controller
    {
        private readonly ILogger<BatchController> _logger;
        private readonly IBatchService _batch;
        private readonly ICommonService _common;
        private readonly IStockService _stock;

        public BatchController(ILogger<BatchController> logger, IBatchService batchService, ICommonService commonService, IStockService stock)
        {
            this._logger = logger;
            this._batch = batchService;
            this._common = commonService;
            this._stock = stock;
        }

        /// <summary>
        /// List all batches as a list
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int? currentPage)
        {
            if (currentPage == null || currentPage < 1)
                currentPage = 1;
            var model = new BatchHomeVM();
            model.PageSize = 5;
            model.CurrentPage = currentPage??1;
            model.BatchList = this._batch.GetAll(model.PageSize, model.CurrentPage);
            model.TotalCount = this._batch.GetTotalCount();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new BatchHomeVM();
            model.AvailableFruits = this._common.GetAllFruits();
            model.AvailableVariety = this._common.GetAllVariety();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BatchDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    this._batch.AddBatch(model);
                    this._stock.AddStock(model);
                    TempData["Message"] = "Created the batch successfully!";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception e)
            {
                TempData["Message"] = "An error occured while processing your request";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int batchId)
        {
            try
            {
                var batch = this._batch.GetDetails(batchId);
                return View(batch);
            }
            catch (Exception e)
            {
                TempData["Message"] = "An error occured while processing your request";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(BatchDTO model)
        {
            try
            {
                model.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var oldQuantity = this._batch.UpdateQuantity(model);
                this._stock.UpdateStock(model, oldQuantity);
                TempData["Message"] = "Successfully updated the quantity of the batch with batched Id " + model.BatchId;
            }
            catch (Exception e)
            {
                TempData["Message"] = "An error occured while processing your request";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int batchId)
        {
            try
            {
                var batch = this._batch.DeleteBatch(batchId);
                var oldQuantity = batch.Quantity;
                batch.Quantity = 0;
                this._stock.UpdateStock(batch, oldQuantity);
                TempData["Message"] = "Successfully deleted the batch with batchedId "+ batchId;
            }
            catch (Exception e)
            {
                TempData["Message"] = "An error occured while processing your request";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
