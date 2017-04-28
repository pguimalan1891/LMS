using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class BookingSvc: IBookingSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IBookingDAO booking = factory.BookingDAO;

        public List<Dictionary<string, object>> getBookingRecords(int status)
        {
            return booking.getBookingRecords(status);
        }

        public List<Dictionary<string, object>> getCheckVoucher(int status)
        {
            return booking.getCheckVoucher(status);
        }

        public List<Dictionary<string, object>> getCIRForm(int status)
        {
            return booking.getCIRForm(status);
        }

        public List<Dictionary<string, object>> getDisbursementVoucher(int status)
        {
            return booking.getDisbursementVoucher(status);
        }

        public List<Dictionary<string, object>> getChangeCCIForm(int status)
        {
            return booking.getChangeCCIForm(status);
        }

        public BusinessObjects.DLRModel getDLR(string lmsno)
        {
            return booking.getDLR(lmsno);
        }
    }
}
