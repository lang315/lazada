using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazada
{
    class ExportResult
    {
        private string GetCurrentDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public void CreateFile(string fileName)
        {
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Sheet1");
            excel.SaveAs(new FileInfo(GetCurrentDirectory() + fileName));
        }

        public void FileResult(string fileName, Account account)
        {
            ExcelPackage excel = new ExcelPackage(new FileInfo(GetCurrentDirectory() + fileName));
            var sheet = excel.Workbook.Worksheets.FirstOrDefault();
            int endRow, endColumn;
            try
            {
                endRow = sheet.Dimension.End.Row;
                endColumn = sheet.Dimension.End.Column;
            }
            catch
            {
                endRow = 1;
                endColumn = 1;
            }

            var row = sheet.Cells[2, 2, endRow, endColumn];
            row[endRow + 1, 1].Value = account.Email;
            row[endRow + 1, 2].Value = account.Password;
            row[endRow + 1, 3].Value = account.IP;
            row[endRow + 1, 4].Value = account.UsernameIP;
            row[endRow + 1, 5].Value = account.PasswordIP;
            excel.Save();
        }

    }
}
