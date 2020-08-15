using System;
using System.Collections.Generic;

namespace Data.Model
{
    public class Stock
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        public string StockCode { get; set; }

        public Stock Parent { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public List<SpecificStock> SpecificStocks { get; set; }


    }
}
