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

namespace CalculateStock.Common.CalculationRelativeReturns
{
    public class CalculationRelativeCore
    {
        DataTable dt = null;
        List<Stock> StockCompositeIndexList = new List<Stock>();

        public CalculationRelativeCore(DataTable dateTable)
        {
            dt = dateTable;
            StockCompositeIndexList= CalculateRoseAndFell("上证指数");
        }
        
        /// <summary>
        /// 根据股票名称计算单日涨跌幅
        /// </summary>
        /// <param name="name">股票名称</param>
        /// <returns>已经计算出单日涨跌幅股票list</returns>
        public List<Stock> CalculateRoseAndFell(string name)
        {
            List<Stock> _stockList = new List<Stock>();
            Decimal yesterdayValue = 0;
            bool isPreviousHaveValue = false;
            for (int i= 0; i < dt.Rows.Count; i++)
            {
                Stock stock = new Stock();
                stock.ShowDate = Convert.ToDateTime(dt.Rows[i]["日期"].ToString());

                if (dt.Rows[i][name] != null&& dt.Rows[i][name]!=DBNull.Value)
                {
                    if (!isPreviousHaveValue)
                    {
                        yesterdayValue = stock.MarketClosePriceToday = Convert.ToDecimal(dt.Rows[i][name].ToString());
                        stock.PriceLimit = 0;
                        isPreviousHaveValue = true;
                    }
                    else
                    { 
                        stock.MarketClosePriceToday = Convert.ToDecimal(dt.Rows[i][name]);
                        stock.PriceLimit = stock.MarketClosePriceToday / yesterdayValue - 1;
                        yesterdayValue = stock.MarketClosePriceToday;
                    }
                }else
                {
                    isPreviousHaveValue = false;
                }
               
                _stockList.Add(stock);
            }
            return _stockList;
        }

        /// <summary>
        /// 根据股票名称计算相对收益
        /// </summary>
        /// <param name="name">股票名称</param>
        /// <returns>已经计算出相对收益股票list</returns>
        public List<Stock> GetCalculationRelative(string name)
        {
            List<Stock> stocks = new List<Stock>();
            List < Stock > CalculationStockList = CalculateRoseAndFell(name);
            Decimal yesterdayRelativeReturns = 0;
            for (int i = 0; i < StockCompositeIndexList.Count; i++)
            {
                Stock stock = new Stock();
                stock.ShowDate = StockCompositeIndexList[i].ShowDate;
                if (i == 0)
                {
                    yesterdayRelativeReturns=stock.RelativeReturns = 1;
                }else
                {
                    //四舍五入
                    stock.RelativeReturns = Math.Round(((CalculationStockList[i].PriceLimit - StockCompositeIndexList[i].PriceLimit) + 1) * yesterdayRelativeReturns, 2, MidpointRounding.AwayFromZero);
                    yesterdayRelativeReturns = stock.RelativeReturns;
                }
                stocks.Add(stock);
            }
            return stocks;
        }
    }
}
