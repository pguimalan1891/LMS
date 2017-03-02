using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
   public interface IBookingSvc
    {
        List<Dictionary<string, object>> getBookingRecords(int status);
        List<Dictionary<string, object>> getCheckVoucher(int status);
        List<Dictionary<string, object>> getCIRForm(int status);
        List<Dictionary<string, object>> getDisbursementVoucher(int status);
    }
}
