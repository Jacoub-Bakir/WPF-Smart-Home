using Dapper;
using RemoteInerfaces.Entities;
using RemoteInerfaces.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class ActionsHistoryService : MarshalByRefObject, IActionsHistory
    {
        DB_Connection connection;
        public ActionsHistoryService()
        {
            connection = new DB_Connection();
        }

        public void saveDeviceAction(DeviceActionsHistory action)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"INSERT INTO device_actions_history (house_id, id_device, id_executer, date_time, action_type) VALUES (@house_id, @id_device, @id_executer, @date_time, @action_type) ", action);
                int action_id = cnn.ExecuteScalar<int>("SELECT LAST_INSERT_ID()");
               
                Console.WriteLine("action saved with success");
            }
        }

        public List<DeviceActionsHistory> getHouseLastActions(int house_id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<DeviceActionsHistory> actions = cnn.Query<DeviceActionsHistory>($"SELECT * FROM device_actions_history WHERE house_id = '{house_id}'").ToList();
                return actions;
            }
        }
    }
}
