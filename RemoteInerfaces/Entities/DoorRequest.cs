using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class DoorRequest
    {
        public int id { get; set; }
        public int house_id { get; set; }
        public string full_name { get; set; }
        public byte[] face_image { get; set; }
        public DateTime request_time { get; set; }
        public Boolean request_responce { get; set; }
        public int responce_by { get; set; }
        public string request_message { get; set; }
        public string responce_message { get; set; }
    }
}
