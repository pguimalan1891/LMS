using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IBookingDAO
    {
        List<Dictionary<string, object>> getBookingRecords(int status);

        List<Dictionary<string, object>> getCheckVoucher(int status);
        List<Dictionary<string, object>> getCIRForm(int status);
        List<Dictionary<string, object>> getDisbursementVoucher(int status);
        List<Dictionary<string, object>> getChangeCCIForm(int status);

    }
}
