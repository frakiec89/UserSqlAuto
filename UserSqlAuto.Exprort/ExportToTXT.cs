using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto.Exprort
{
    public class ExportToTXT : IExportToFile
    {
        public bool ExportTXT(string path , string content  )
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(content);
                }
                return true;
            }
            catch
            {
                return false;
            }
           

        }
    }
}
