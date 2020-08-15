using CalculateStock.Common;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class RelativeProfitRepository: IRelativeProfitRepository
    {
        public List<Stock> GetStockData()
        {
            //从Excel中得到数据。
            string dataSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excel", "StockData.xlsx");
            List<Stock> stocks=  ExcelHelper.ExcelToList(dataSourcePath);
            return stocks;
        }
    }
}
