using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IAccountDAO
    {
        List<Dictionary<string, object>> Login(string username, string password);
        List<Dictionary<string, object>> getMenus();
    }
}
