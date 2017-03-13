using System;
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

    public class newLoanBorrowerProfile
    {
        public string newLoanLink;
        public string id;
        public string Status;
        public string Code;
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string DateofBirth;
        public string Gender;
        public string CivilStatus;
        public string Address;
        public string City;
    }

    public class RequiredDocuments
    {
        public string id;
        public string required_document_id;
        public string support_document_id;
        public string description;
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

    public class DocumentStatus
    {
        public string CODE;
        public string DESCRIPTION;
        
    }

    public class LoanList
    {
        public string ID;
        public string Status;
        public string LA_No;
        public string Date;
        public string Branch;
        public string Customer;
        public string CustomerAddress;
        public string Product;
        public string Desired;
        public string Recommended;
        public string Approved;
        public string Set;
        public string Terms;
        public string Purpose;
        public string CCI;
        public string CiStatus;
    }

    public class Collateral
    {
        public string collateralId;
        public string MaxLoanValue;
        public string AppraisedValue;
        public string LoanValue;
        public string CollateralGroup;
        public string CollateralType;
        public string Description;
        public string Usage;
        public string TCTNo;
        public string Year;
        public string Model;
        public string Color;
        public string SerialNo;
        public string Fuel;
        public string ChassisNo;
        public string EngineNo;
        public string PlateNo;
        public string OdoReading;
        public string CRENo;
        public string CREName;
        public string CRExpiryDate;
        public string ORNo;
        public string ORExpiryDate;
        public string InsuranceName;
        public string InsuranceExpiryDate;


    }

    public class Comaker
    {
        public string ComakerId;
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public DateTime BirthDate;
        public string Address;
        public string PhoneNo;
        public string Notes;

    }
    public class LoanApplicationModel
    {
        public string reference_id;

        public string AccountNo;

        public DateTime TransactionDate;

        public string BorrowerCode;

        public string LoanPurpose;

        public string DistrictCode;

        public string BranchCode;

        public string ApplicationType;

        public string AgentId;

        public string ProductId;

        public string SetId;

        public string TermsId;

        public string FactorRate;

        public string DesiredMLV;



        public List<Comaker> ListOfComakers;



        public List<Collateral> ListOfCollaterals;

        public string Notes;

        public string ResultStatus;

        public IEnumerable<BusinessObjects.Organization> orgs;
        public BusinessObjects.BorrowerProfile borrowerProfile;
        public IEnumerable<BusinessObjects.District> districts;
        public IEnumerable<BusinessObjects.ApplicationType> applicationTypes;
        public IEnumerable<BusinessObjects.LoanType> products;
        public IEnumerable<BusinessObjects.LoanSet> sets;
        public IEnumerable<BusinessObjects.LoanTerms> terms;

        public IEnumerable<BusinessObjects.RequiredDocuments> reqDocs;



    }
}
