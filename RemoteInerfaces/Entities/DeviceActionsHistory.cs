using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class DeviceActionsHistory
    {
        public int id { get; set; }
        public int house_id { get; set; }
        public int id_device { get; set; }
        public int id_executer { get; set; }
        public DateTime date_time { get; set; }
        public string action_type { get; set; }
    }
}
