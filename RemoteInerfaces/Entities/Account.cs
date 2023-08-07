using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Entities
{
    [Serializable]
    public class Account
    {

        public int Id { get; set; }
        public int id_house { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime BirthDate { get; set; } 
        public string Privilege { get; set; }
        public string Password { get; set; }
        public byte[] face_ID { get; set; }
        public byte[] profile_image { get; set; }
        public Boolean is_Active { get; set; }
        

    }
}
