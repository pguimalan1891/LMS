using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAccountSvc
    {
        List<Dictionary<string, object>> Login(string username, string password);
        List<Dictionary<string, object>> getMenus();
    }
}
