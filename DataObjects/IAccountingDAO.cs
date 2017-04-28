using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IAccountingDAO
    {
        List<Dictionary<string, object>> getRequestForPayment(int status);
        List<Dictionary<string, object>> getExpenseType();
    }
}
