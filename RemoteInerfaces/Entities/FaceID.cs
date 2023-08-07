using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class FaceID
    {
        public int id { get; set; }
        public byte[] face_id { get; set; }
    }
}
