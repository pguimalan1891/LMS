using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using BusinessObjects;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class OfficialReceiptSvc : IOfficialReceiptSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IOfficialReceiptDAO OfficialReceipt = factory.OfficialReceiptDAO;

        public List<Dictionary<string, object>> getDLRActiveAccounts()
        {
            return OfficialReceipt.getDLRActiveAccounts();
        }

        public IEnumerable<PaymentMode> getPaymentMode()
        {
            return OfficialReceipt.getPaymentMode();
        }

        public IEnumerable<Bank> getBank()
        {
            return OfficialReceipt.getBank();
        }

        public List<Dictionary<string, object>> getCollectionDues(string DLRNumber)
        {
            return OfficialReceipt.getCollectionDues(DLRNumber);
        }

        public string getServerDate()
        {
            return OfficialReceipt.getServerDate();
        }

        public int SubmitOfficialReceipt(OfficialReceipt OfficialReceiptModel)
        {
            return OfficialReceipt.SubmitOfficialReceipt(OfficialReceiptModel);
        }
    }
}
