using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces
{
    public interface DoorRequestsInterface
    {
        void respondToRequest(int request_id, int responce_by, Boolean positiveResponce, String ResponceMessage);
        
    }
}
