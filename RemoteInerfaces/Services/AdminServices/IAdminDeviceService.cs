using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services.AdminServices
{
    public interface IAdminDeviceService
    {
        void addNewDevice(Device device);
        List<Device> getRoomDevices(int id_room);
        Device getDevice(int id_device);
        void changeDeviceState(int house_id, int id_executer, int id_device, bool isActive);
        Dictionary<Room, Device> getActiveHouseDevices(int house_id);
    }
}
