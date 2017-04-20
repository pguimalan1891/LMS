using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Collection
{
    public class SundryViewModel
    {
        public Sundry SundryDetails { get; set; }
        public IEnumerable<CMDMAccountType> CMDMAccountType { get; set; }
    }

    public class SundryOfficialReceipt
    {
        public OfficialReceipt OfficialReceipt { get; set; }
        public IEnumerable<Sundry> SundryAccounts { get; set; }
    }

    public class CMDMAccountType
    {
        public string CMDMAccountTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }   

    public class Sundry
    {
        public string ID { get; set; }
        public string CMDMAccountTypeID { get; set; }
        public string SundryAmount { get; set; }
    }
}