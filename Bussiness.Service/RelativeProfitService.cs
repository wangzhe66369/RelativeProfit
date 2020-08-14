using Bussiness.Interface;
using CalculateStock.Common.CalculationRelativeReturns;
using Data.Model;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Service
{
    public class RelativeProfitService : IRelativeProfitService
    {
        private readonly IRelativeProfitRepository _IRelativeProfitRepository;
        public RelativeProfitService(IRelativeProfitRepository iRelativeProfitRepository)
        {
            _IRelativeProfitRepository = iRelativeProfitRepository;
        }
        /// <summary>
        /// 获得股票信息
        /// </summary>
        /// <param name="stockValue">股票名字</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>股票信息list</returns>
        public List<Stock> GetStockData(string stockValue, string startDate, string endDate)
        {
            DataTable dataTable =_IRelativeProfitRepository.GetStockData();

            string tmpStock = string.Empty;
            switch (stockValue)
            {
                case "PingAnBank":
                    tmpStock = "平安银行(000001)";
                    break;
                case "KweichowMoutai":
                    tmpStock = "贵州茅台(600519)";
                    break;
                case "ChinaCITIC":
                    tmpStock = "中信建投(601066)";
                    break;
                case "HuaxingYuanchuang":
                    tmpStock = "华兴源创(688001)";
                    break;
                case "TongdaVenture":
                    tmpStock = "同达创业(600647)";
                    break;
                default:
                    break;
            }

            CalculationRelativeCore calculationRelativeCore = new CalculationRelativeCore(dataTable);
            var stockList = calculationRelativeCore.GetCalculationRelative(tmpStock).AsQueryable();

            if(!string.IsNullOrEmpty(startDate))
            {
                stockList=stockList.Where(d => d.ShowDate >= Convert.ToDateTime(startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                stockList=stockList.Where(d => d.ShowDate <= Convert.ToDateTime(endDate));
            }
            return stockList.ToList();
        }

        /// <summary>
        /// 根据不同的股票名称获取相应的日期和相对收益
        /// </summary>
        /// <param name="stockValue">股票名字</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, string[]> GetDateAndRelativeProfit(string stockValue, string startDate, string endDate)
        {
            var stocList = GetStockData(stockValue, startDate, endDate);
            Dictionary<string, string[]> valuePairs = new Dictionary<string, string[]>();

            string[] arrDate = new string[stocList.Count];
            string[] arrRelativeProfit = new string[stocList.Count];
            int i = 0;
            foreach (var item in stocList)
            {
                arrDate[i] = item.ShowDate.ToString("yyyy/MM/dd");
                arrRelativeProfit[i] = item.RelativeReturns.ToString();
                i++;
            }
            valuePairs.Add("ShowDate", arrDate);
            valuePairs.Add("RelativeReturns", arrRelativeProfit);
            return valuePairs;
        }
    }
}
