using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.LoanApplication
{
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

        //functions

        public LoanApplicationModel submit()
        {
            
            if(this.AccountNo != null)
            {
                //Update | Delete


            }else
            {
                //Insert
                
                foreach(Collateral col in ListOfCollaterals)
                {
                    processCollaterals(col, 0);
                }

                foreach (Comaker com in ListOfComakers)
                {
                    processComakers(com, 0);
                    
                }
            }

            return this;
        }

        public List<Collateral> processCollaterals(Collateral parm, int MultipleProcessFlag = 1)
        {
            if (parm != null)
            {
                if (this.AccountNo != null)
                {
                    if (parm.collateralId != null)
                    {
                        //insert Collateral
                        this.ListOfCollaterals  = processCollaterals(null);
                        return null;
                    }
                    else
                    {
                        //update or Delete Collateral
                        this.ListOfCollaterals = processCollaterals(null);
                        return null;
                    }
                }
                else
                {
                    //update or Delete Temporary Collateral List
                    ListOfCollaterals.Add(parm);
                    return ListOfCollaterals;
                }

            }
            else
            {
                //for Multiple Insert due to temporary List
                if (MultipleProcessFlag == 0)
                {
                    return null;
                }

                //get all Collateral
                return null;
            }


        }
        public List<Comaker> processComakers(Comaker parm , int MultipleProcessFlag =1)
        {
            if(parm != null )
            {
                if (this.AccountNo != null)
                {
                    if (parm.ComakerId != null)
                    {
                        //insert Comaker
                        this.ListOfComakers = processComakers(null);
                        return null;
                    }
                    else
                    {
                        //update or Delete Comaker
                        this.ListOfComakers = processComakers(null);
                        return null;
                    }
                }
                else
                {
                    //update or Delete Temporary Comaker List
                    ListOfComakers.Add(parm);
                    return ListOfComakers;
                }

            }else
            {
                //for Multiple Insert due to temporary List
                if(MultipleProcessFlag == 0)
                {
                    return null;
                }

                //get all Comakers
                return null;
            }
            
        }

    }
}