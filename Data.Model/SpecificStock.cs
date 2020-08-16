using System;

namespace Data.Model
{
    public class SpecificStock
    {
        public DateTime Date { get; set; }

        public decimal? Price { get; set; }
        /// <summary>
        /// 单日涨幅
        /// </summary>
        public decimal OneDayPriceLimit { get; set; }

        /// <summary>
        /// 相对收益
        /// </summary>
        public decimal RelativeProfit { get; set; }

        public Stock stock { get; set; }
    }
}
