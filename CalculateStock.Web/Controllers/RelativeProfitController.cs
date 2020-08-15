using Bussiness.Service;
using CalculateStock.Web.Models;
using Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculateStock.Controllers
{
    public class RelativeProfitController : Controller
    {
        private readonly IRelativeProfitService _IRelativeProfitService;
        public RelativeProfitController(IRelativeProfitService iRelativeProfitService)
        {

            _IRelativeProfitService = iRelativeProfitService;
        }

        public ActionResult Index()
        {
            var stoctVlue= _IRelativeProfitService.GetStockNameList();
            List<StockName> RadioItemList = new List<StockName>();

            foreach (var item in stoctVlue)
            {
                StockName stockName = new StockName();
                stockName.StockCode = item.StockCode;
                stockName.Name = item.Name;
                RadioItemList.Add(stockName);
            }

            StockVM stoctVM = new StockVM();
            stoctVM.RadioItemList = RadioItemList;
           
            return View(stoctVM);
        }

        public ActionResult GetStockData(string stockValue, string startDate, string endDate)
        {
            var valuePairs = _IRelativeProfitService.GetDateAndRelativeProfit(stockValue, startDate, endDate);
            string jsonData = JsonConvert.SerializeObject(valuePairs);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
       
    }
}
