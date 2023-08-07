using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class RoomWithDevieces
    {
        public Room room { get; set; }
        public List<Device> room_devices { get; set; }
    }
}
