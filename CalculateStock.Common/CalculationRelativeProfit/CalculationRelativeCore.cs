using Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CalculateStock.Common.CalculationRelativeProfit
{
    public class CalculationRelativeCore
    {
        List<Stock> stockList = new List<Stock>();
        List<SpecificStock> StockCompositeIndexList = new List<SpecificStock>();
       
        public CalculationRelativeCore(List<Stock> _stockList)
        {
            stockList = _stockList;
            StockCompositeIndexList = CalculateRoseAndFell(string.Empty);
        }
        
        /// <summary>
        /// 根据股票名称计算单日涨跌幅
        /// </summary>
        /// <param name="name">股票名称</param>
        /// <returns>已经计算出单日涨跌幅股票list</returns>
        public List<SpecificStock> CalculateRoseAndFell(string stockCode)
        {
            Stock stock = null;
            if (string.IsNullOrEmpty(stockCode))
            {
                stock = stockList.FirstOrDefault();
            }
            else
            {
                stock = stockList.Where(d=>d.StockCode== stockCode).FirstOrDefault();
            }

            List<SpecificStock> specificStockList = stock.SpecificStocks;
            bool isPreviousHaveValue = false;
            double? yesterdayValue = 0;
            foreach (var specificStock in specificStockList)
            {
                if (specificStock.Price != null)
                {
                    if (!isPreviousHaveValue)
                    {
                        yesterdayValue = specificStock.Price;
                        specificStock.OneDayPriceLimit = 0;
                        isPreviousHaveValue = true;
                    }
                    else
                    {
                        specificStock.OneDayPriceLimit = Convert.ToDecimal(specificStock.Price / yesterdayValue - 1);
                        yesterdayValue = specificStock.Price;
                    }
                }
                else
                {
                    isPreviousHaveValue = false;
                }
            }
            return specificStockList;
        }

        /// <summary>
        /// 根据股票名称计算相对收益
        /// </summary>
        /// <param name="name">股票名称</param>
        /// <returns>已经计算出相对收益股票list</returns>
        public List<SpecificStock> GetCalculationRelative(string stockCode)
        {
            List <SpecificStock> CalculationStockList = CalculateRoseAndFell(stockCode);
            Decimal yesterdayRelativeProfit = 0;
            int i = 0;
            foreach (var specificStock in CalculationStockList)
            {
                if (i == 0)
                {
                    yesterdayRelativeProfit = specificStock.RelativeProfit = 1;
                }
                else
                {
                    //四舍五入
                    specificStock.RelativeProfit = Math.Round(((CalculationStockList[i].OneDayPriceLimit - StockCompositeIndexList[i].OneDayPriceLimit) + 1) * yesterdayRelativeProfit, 2, MidpointRounding.AwayFromZero);
                    yesterdayRelativeProfit = specificStock.RelativeProfit;
                }
                i++;
            }
            return CalculationStockList;
        }
    }
}
