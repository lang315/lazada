using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lazada
{
    class GenerateListAccount
    {
        public bool IsOpening { get; set; }
        private string PathFileAccount { get; set; }
        ExcelPackage excel = new ExcelPackage();

        public GenerateListAccount(string pathFileAccount)
        {

            PathFileAccount = pathFileAccount;
            try
            {
                excel = new ExcelPackage(new FileInfo(PathFileAccount));
                IsOpening = false;
            }
            catch
            {
                IsOpening = true;
            }
        }

        public List<Account> Queue(bool onlyLikeProduct, List<string> listSSH)
        {
            if (IsOpening)
                return null;
            List<Account> lst = new List<Account>();
            var sheet = excel.Workbook.Worksheets.FirstOrDefault();
            int countSSH = listSSH.Count;
            int indexSSH = 0;
            string ssh = string.Empty;
            if (sheet != null)
            {
                for (int i = 2; i <= sheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        if (indexSSH >= countSSH)
                            break;
                        ssh = listSSH[indexSSH];
                        indexSSH++;
                        var row = sheet.Cells[i, 1, i, sheet.Dimension.End.Column];
                        var account = new Account(onlyLikeProduct, row[i, 1].Value.ToString(), row[i, 2].Value.ToString(), ssh.Split('|')[0], ssh.Split('|')[1], ssh.Split('|')[2]);
                        lst.Add(account);
                    }
                    catch
                    {

                    }
                }
            }
            return lst;
        }
    }
}
