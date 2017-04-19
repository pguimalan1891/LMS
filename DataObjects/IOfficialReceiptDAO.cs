using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IOfficialReceiptDAO
    {
        List<Dictionary<string, object>> getDLRActiveAccounts();
        IEnumerable<PaymentMode> getPaymentMode();
        IEnumerable<Bank> getBank();
        List<Dictionary<string, object>> getCollectionDues(string DLRNumber);
        string getServerDate();
        int SubmitOfficialReceipt(OfficialReceipt OfficialReceiptModel);
    }
}
