using CalculateStock.Common.CalculationRelativeProfit;
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
        /// 获得具体的股票，用于页面显示
        /// </summary>
        /// <returns></returns>
        public List<Stock> GetStockNameList()
        {
            var stocks = _IRelativeProfitRepository.GetStockData().Where(d=>d.Parent!=null).ToList();
            return stocks;

        }

        /// <summary>
        /// 获得股票信息
        /// </summary>
        /// <param name="stockValue">股票名字</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>股票信息list</returns>
        public List<SpecificStock> GetStockData(string stockCode, string startDate, string endDate)
        {
            List<Stock> stocks = _IRelativeProfitRepository.GetStockData();

            CalculationRelativeCore calculationRelativeCore = new CalculationRelativeCore(stocks);
            var specificStocks = calculationRelativeCore.GetCalculationRelative(stockCode).AsQueryable();

            if (!string.IsNullOrEmpty(startDate))
            {
                specificStocks = specificStocks.Where(d => d.Date >= Convert.ToDateTime(startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                specificStocks = specificStocks.Where(d => d.Date <= Convert.ToDateTime(endDate));
            }
            return specificStocks.ToList();
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
                arrDate[i] = item.Date.ToString("yyyy/MM/dd");
                //页面显示的数据,四舍五入
                arrRelativeProfit[i] = Math.Round(item.RelativeProfit, 2, MidpointRounding.AwayFromZero).ToString();
                i++;
            }
            valuePairs.Add("ShowDate", arrDate);
            valuePairs.Add("RelativeProfit", arrRelativeProfit);
            return valuePairs;
        }
    }
}
