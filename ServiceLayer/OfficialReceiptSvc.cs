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

        public IEnumerable<CMDMAccountType> getCMDMAccountType()
        {
            return OfficialReceipt.getCMDMAccountType();
        }

        public int SubmitSundry(OfficialReceipt OfficialReceiptModel, IEnumerable<Sundry> SundryAccount)
        {
            return OfficialReceipt.SubmitSundry(OfficialReceiptModel, SundryAccount);
        }

        public List<Dictionary<string, object>> getOfficialReceiptListing(string Status, string CustomerName)
        {
            return OfficialReceipt.getOfficialReceiptListing(Status, CustomerName);
        }

        public OfficialReceipt getOfficialReceipt(string ORNumber)
        {
            return OfficialReceipt.getOfficialReceipt(ORNumber).First();
        }

        public IEnumerable<Sundry> getSundry(string ORNumber)
        {
            return OfficialReceipt.getSundry(ORNumber);
        }
        public int UpdateOfficialReceipt(OfficialReceipt OfficialReceiptModel, string isFinalize)
        {
            return OfficialReceipt.UpdateOfficialReceipt(OfficialReceiptModel, isFinalize);
        }
    }
}
