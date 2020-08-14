using Bussiness.Interface;
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
            return View();
        }

        public ActionResult GetStockData(string stockValue, string startDate, string endDate)
        {
            var valuePairs = _IRelativeProfitService.GetDateAndRelativeProfit(stockValue, startDate, endDate);
            string jsonData = JsonConvert.SerializeObject(valuePairs);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
       
    }
}
