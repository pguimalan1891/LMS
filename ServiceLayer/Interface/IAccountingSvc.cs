using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAccountingSvc
    {
        List<Dictionary<string, object>> getRequestForPayment(int status);
    }
}
