using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class Device
    {
        public int id_device { get; set; }
        public double id_room { get; set; }
        public string descriptive_name { get; set; }
        public string type { get; set; }
        public Boolean state { get; set; }
        public string url { get; set; }
        
    }
    public enum Type { Camera, TV, Temperature_Humidity, Light, Air_Conditioner, Refridgerator, Door }
}
