

using System;
using System.Collections.Generic;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class Room
    {
        public int id_room { get; set; }
        public double id_house { get; set; }
        public string descriptive_name { get; set; }
        public byte[] background_image { get; set; }
    }
}
