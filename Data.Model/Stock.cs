using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    /// <summary>
    /// 抽象成股票
    /// </summary>
    public class Stock
    {
        public decimal MarketClosePriceToday { get; set; }
        public DateTime ShowDate { get; set; }
        public decimal PriceLimit { get; set; }
        public decimal RelativeReturns { get; set; }
    }
}
