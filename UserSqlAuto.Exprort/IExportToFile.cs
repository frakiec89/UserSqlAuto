using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto.Exprort
{
    public interface IExportToFile
    {
        bool ExportTXT(string path , string content);
    }
}
