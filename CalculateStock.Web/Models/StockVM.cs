using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculateStock.Web.Models
{
    public class StockVM
    {
        public List<StockName> RadioItemList { get; set; }
        
    }
    public class StockName
    {
        public string StockCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}