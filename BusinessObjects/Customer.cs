using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Customer
    {
    }
    public class Orgranization
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DistrictID { get; set; }
        public string MotherBranchID { get; set; }
        public string BaseTable { get; set; }
        
        public Orgranization()
        {
            BaseTable = "organization";
        }
    }
    public class District
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DistrictGroupID { get; set; }
        public string BaseTable { get; set; }

        public District()
        {
            BaseTable = "district";
        }
    }
    public class Gender
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }        
        public string BaseTable { get; set; }

        public Gender()
        {
            BaseTable = "gender";
        }
    }
    public class CivilStatus
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string BaseTable { get; set; }

        public CivilStatus()
        {
            BaseTable = "civil_status";
        }
    }
    public class Citizenship
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string BaseTable { get; set; }

        public Citizenship()
        {
            BaseTable = "citizenship";
        }
    }


}
