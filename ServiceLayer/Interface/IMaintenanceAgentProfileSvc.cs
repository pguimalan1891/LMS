using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace ServiceLayer.Interface
{
    public interface IMaintenanceAgentProfileSvc
    {
        string getAgentCodebyID(string ID);
        int UpdateAgentData(string ProcessType, AgentModel custModel, string AgentProfileID);
        List<Dictionary<string, object>> getAgentProfileList();
        BusinessObjects.AgentProfile getAgentProfilebyCode(string Code);
        IEnumerable<BusinessObjects.AgentAddress> getAgentAddressByID(string ID);
        IEnumerable<City> getCity(string AgentProfileID);
        IEnumerable<AgentType> getAgentType();

    }
}
