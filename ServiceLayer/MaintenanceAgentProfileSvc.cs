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
    public class MaintenanceAgentProfileSvc : IMaintenanceAgentProfileSvc
    {
        static readonly IDAOFactory factory = DAOFactories.GetFactory();
        static readonly IMaintenanceAgentProfileDAO MaintenanceAgentProfile = factory.MaintenanceAgentProfileDAO;

        public string getAgentCodebyID(string ID)
        {
            return MaintenanceAgentProfile.getAgentCodebyID(ID);
        }

        public int UpdateAgentData(string ProcessType, AgentModel AgentModel, string AgentProfileID)
        {
            return MaintenanceAgentProfile.UpdateAgentData(ProcessType, AgentModel, AgentProfileID);
        }
        public List<Dictionary<string, object>> getAgentProfileList()
        {
            return MaintenanceAgentProfile.getAgentProfileList();
        }

        public BusinessObjects.AgentProfile getAgentProfilebyCode(string Code)
        {
            var AgentProfile = MaintenanceAgentProfile.getAgentProfilebyCode(Code);
            return (BusinessObjects.AgentProfile)AgentProfile.First();
        }

        public IEnumerable<BusinessObjects.AgentAddress> getAgentAddressByID(string ID)
        {
            return MaintenanceAgentProfile.getAgentAddressByID(ID);
        }

        public IEnumerable<City> getCity(string AgentProfileID)
        {
            return MaintenanceAgentProfile.getCity(AgentProfileID);
        }

        public IEnumerable<AgentType> getAgentType()
        {
            return MaintenanceAgentProfile.getAgentType();
        }
    }
}
