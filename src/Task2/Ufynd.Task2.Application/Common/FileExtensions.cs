using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Linq;
using Ufynd.Task2.Application.Common.Models;

namespace Ufynd.Task2.Application.Common
{
    public static class FileExtensions
    {
        public static XSSFWorkbook ToWorkBook(this HotelJsonModel hotelJsonModel)
        {
            var hotelRates = hotelJsonModel.hotelRates;
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(hotelJsonModel.hotel.name);

            sheet.SetAutoFilter(CellRangeAddress.ValueOf("A1:G1"));
            sheet.CreateFreezePane(0, 1);

            var headerRow = sheet.CreateRow(0);
            CreateHeaderRow(headerRow);

            for (var i = 0; i < hotelRates.Length; i++)
            {
                var row = sheet.CreateRow(i + 1);
                var currentRate = hotelRates[i];
                FillRowDate(currentRate, row);
            }

            SetAutoSizeColumns(sheet);

            return workbook;
        }

        public static MemoryStream ToMemoryStream(this XSSFWorkbook workbook)
        {
            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream, true);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static string ToFile(this XSSFWorkbook workbook, string fileAddress)
        {
            using (FileStream file = new FileStream(fileAddress, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(file);
            }

            return fileAddress;
        }

        private static void FillRowDate(Hotelrate currentRate, IRow row)
        {
            row.CreateCell(0).SetCellValue(currentRate.targetDay.ToMyDateFormat());
            row.CreateCell(1).SetCellValue(currentRate.targetDay.AddDays(currentRate.los).ToMyDateFormat());
            row.CreateCell(2).SetCellValue(currentRate.price.numericFloat.ToMyPriceFormat());
            row.CreateCell(3).SetCellValue(currentRate.price.currency);
            row.CreateCell(4).SetCellValue(currentRate.rateName);
            row.CreateCell(5).SetCellValue(currentRate.adults);

            int breakfastCondition = currentRate.rateTags.Any(r => r.name == "breakfast" && r.shape == true) ? 1 : 0;
            row.CreateCell(6).SetCellValue(breakfastCondition);
        }

        private static void SetAutoSizeColumns(ISheet sheet)
        {
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);
            sheet.AutoSizeColumn(4);
            sheet.AutoSizeColumn(5);
            sheet.AutoSizeColumn(6);
        }

        private static void CreateHeaderRow(IRow headerRow)
        {
            headerRow.CreateCell(0, CellType.String).SetCellValue("ARRIVAL_DATE");
            headerRow.CreateCell(1, CellType.String).SetCellValue("DEPARTURE_DATE");
            headerRow.CreateCell(2, CellType.String).SetCellValue("PRICE");
            headerRow.CreateCell(3, CellType.String).SetCellValue("CURRENCY");
            headerRow.CreateCell(4, CellType.String).SetCellValue("RATENAME");
            headerRow.CreateCell(5, CellType.Numeric).SetCellValue("ADULTS");
            headerRow.CreateCell(6, CellType.Numeric).SetCellValue("BREAKFAST_INCLUDED");
        }
    }
}