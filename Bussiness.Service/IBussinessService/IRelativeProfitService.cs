using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Service
{
    public interface IRelativeProfitService
    {
        List<Stock> GetStockNameList();

        List<SpecificStock> GetStockData(string stockValue, string startDate, string endDate);

        Dictionary<string, string[]> GetDateAndRelativeProfit(string stockValue, string startDate, string endDate);
    }
}
