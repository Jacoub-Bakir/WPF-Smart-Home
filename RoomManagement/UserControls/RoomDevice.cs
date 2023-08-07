using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.UserControls
{
    public class RoomDevice
    {
        public string Title { get; set; }
        public string ImageOffSource { get; set; }
        public string ImageOnSource { get; set; }
        public int ColumnGrid { get; set; }
        public int RowGrid { get; set; }
        public int Device_ID { get; set; }
        public bool is_active { get; set; }
    }
}
