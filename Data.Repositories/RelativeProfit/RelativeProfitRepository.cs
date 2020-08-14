using CalculateStock.Common;
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
        public DataTable GetStockData()
        {
            //从Excel中得到数据。
            string dataSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excel", "StockData.xlsx");
            DataTable dt = ExcelHelper.ExcelToDataTable(dataSourcePath, "数据", true);
            return dt;
        }
    }
}
