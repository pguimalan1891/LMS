﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class BorrowerProfile
    {
        public string CODE;
        public string Fullname;
        public string LandLine;
        public string MobileNumber;
        public string ProvinceCity;
        public string Barangay;
        public string STREET_ADDRESS;
        public string POSTAL_CODE;
    }

    public class LoanType
    {
        public string ID;
        public string CODE;
        public string DESCRIPTION;
        public string REQUIRE_COLLATERAL;
        public string WITH_DST;
        public string WITH_CV;
        public string BALLOON_PAYMENT;
        public string MIGID;
    }

    public class LoanSet
    {
        public string ID;
        public string CODE;
        public string DESCRIPTION;
        public string MIGID;
    }

    public class LoanTerms
    {
        public string ID;
        public string CODE;
        public string DESCRIPTION;
        public string MONTHS;
        public string MIGID;
    }
}
