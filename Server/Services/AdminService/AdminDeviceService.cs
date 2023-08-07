using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteInerfaces.Entities;
using RemoteInerfaces.Services.AdminServices;
using RemoteInerfaces.Services;

namespace Server.Services.AdminService
{
     class AdminDeviceService : MarshalByRefObject, IAdminDeviceService
    {
        DB_Connection connection;
        ActionsHistoryService actionsHistoryService;
        AdminRoomService adminRoomService;

        public AdminDeviceService()
        {
            actionsHistoryService = new ActionsHistoryService();
            adminRoomService = new AdminRoomService();
            connection = new DB_Connection();
        }

        public void addNewDevice(Device device)
        {
            using (IDbConnection cnn = connection.get_connection())
            {

                cnn.ExecuteScalar($"INSERT INTO device (id_room, descriptive_name, type, state, url) VALUES (@id_room, @descriptive_name, @type, @state, @url)", device);
                Console.WriteLine("device added with success");
            }
        }

        public List<Device> getRoomDevices(int id_room)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<Device> devices = cnn.Query<Device>($"SELECT * FROM device WHERE id_room = '{id_room}'").ToList();
                return devices;
            }
        }

        public Dictionary<Room, Device> getActiveHouseDevices(int house_id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                Dictionary<Room, Device> activeDevices = new Dictionary<Room, Device> ();
                List<RoomWithDevieces> houseRooms = adminRoomService.getAdminRooms(house_id);
                foreach(RoomWithDevieces houseRoom in houseRooms)
                {
                    foreach(Device device in houseRoom.room_devices)
                    {
                        if (device.state)
                        {
                            activeDevices.Add(houseRoom.room, device);
                        }
                    }
                }
                return activeDevices;
            }
        }


        public Device getDevice(int id_device)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                Device device = cnn.Query<Device>($"SELECT * FROM device WHERE id_device = '{id_device}'").FirstOrDefault();
                return device;
            }
        }

        public void changeDeviceState(int house_id, int id_executer, int id_device, bool isActive)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                DeviceActionsHistory deviceActionsHistory = new DeviceActionsHistory();
                deviceActionsHistory.house_id = house_id;
                deviceActionsHistory.id_executer = id_executer;
                deviceActionsHistory.id_device = id_device;
                deviceActionsHistory.action_type = isActive ? "Turned On" : "Turned Off";
                deviceActionsHistory.date_time = DateTime.Now;
                actionsHistoryService.saveDeviceAction(deviceActionsHistory);
                cnn.ExecuteScalar($"UPDATE device SET state = {isActive} WHERE id_device = '{id_device}'");
                Console.WriteLine("device state changed with success");
            }
            string device_url = getDevice(id_device).url;
            IDeviceService DeviceService = (IDeviceService)Activator.GetObject(typeof(IDeviceService), device_url);
            DeviceService.changeDeviceState(isActive);
        }
    }
}
