using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IMaintenanceAgentProfileDAO
    {
        string getAgentCodebyID(string ID);
        List<Dictionary<string, object>> getAgentProfileList();
        int UpdateAgentData(string ProcessType, AgentModel AgentModel, string AgentProfileID);
        IEnumerable<AgentAddress> getAgentAddressByID(string ID);
        IEnumerable<AgentProfile> getAgentProfilebyCode(string Code);
        IEnumerable<City> getCity(string AgentProfileID);
        IEnumerable<AgentType> getAgentType();

    }
}
