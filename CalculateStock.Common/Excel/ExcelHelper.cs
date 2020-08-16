using Data.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateStock.Common.Excel
{
    public class ExcelHelper
    {
        public static List<Stock> ExcelToList(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = WorkbookFactory.Create(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    IRow cells = sheet.GetRow(0);
                    List<Stock> stocks = new List<Stock>();
                    for (int i = 1; i < cells.LastCellNum; i++)
                    {
                        var str = cells.GetCell(i).StringCellValue;
                        var strSpilt = str.Split('(');
                        Stock stock = new Stock()
                        {
                            Name = str,
                            StockCode = strSpilt.Length > 1 ? strSpilt[1].TrimEnd(')') : "",
                            SpecificStocks = new List<SpecificStock>(),
                        };
                        if (i != 1)
                        {
                            stock.Parent = stocks[0];
                        }
                        stocks.Add(stock);
                    }

                    for (int i = 1; i < sheet.LastRowNum; i++)
                    {
                        IRow cellPrices = sheet.GetRow(i);
                        for (int j = 1; j <= stocks.Count; j++)
                        {
                            SpecificStock specificStock = new SpecificStock()
                            {
                                Date = cellPrices.GetCell(0).DateCellValue,
                                Price = cellPrices.GetCell(j)?.NumericCellValue
                            };
                            stocks[j - 1].SpecificStocks.Add(specificStock);
                        }
                    }
                    return stocks;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
