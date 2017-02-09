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
    }
}
