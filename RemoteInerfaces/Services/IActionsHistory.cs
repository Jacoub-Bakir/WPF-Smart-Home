using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services
{
    public interface IActionsHistory
    {
        void saveDeviceAction(DeviceActionsHistory action);
        List<DeviceActionsHistory> getHouseLastActions(int house_id);
    }
}
