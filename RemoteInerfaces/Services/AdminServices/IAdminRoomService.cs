using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services.AdminServices
{
    public interface IAdminRoomService
    {
        void createNewRoom(Room room, List<RoomAccessors> roomAccessors);
        List<RoomWithDevieces> getAdminRooms(int id_house);
        List<RoomWithDevieces> getResidentRooms(int id_account);
    }
}
