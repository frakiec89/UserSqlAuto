using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSqlAuto.BL
{
    public interface IGogs
    {
        void AddUser(string name, string password , string url);
    }
}
