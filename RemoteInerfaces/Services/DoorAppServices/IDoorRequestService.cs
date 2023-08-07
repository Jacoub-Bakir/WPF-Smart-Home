using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services.DoorAppServices
{
    public interface IDoorRequestService
    {
        void saveDoorRequest(DoorRequest request);
        void respondToDoorRequest(int responce_by, Boolean request_responce, String responce_message);
        DoorRequest getRequest(int id);
        List<DoorRequest> getHouseRequest(int house_id);
        void cancelDoorRequest();
        DoorRequest getLastRequest();
    }
}
