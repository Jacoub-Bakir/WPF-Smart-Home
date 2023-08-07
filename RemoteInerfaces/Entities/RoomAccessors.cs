using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class RoomAccessors
    {
        public int id_room { get; set; }
        public int id_account { get; set; }
    }
}
