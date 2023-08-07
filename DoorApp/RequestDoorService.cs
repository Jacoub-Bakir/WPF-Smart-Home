using RemoteInerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorApp
{
    class RequestDoorService : MarshalByRefObject, DoorRequestsInterface
    {

        public static int request_id;
        public static int responce_by;
        public static bool positiveResponce;
        public static string ResponceMessage;
        public static bool isThereResponses = false;
        

        public void respondToRequest(int requestId, int responceBy, bool positive_Responce, string Responce_Message)
        {
            request_id = requestId;
            responce_by = responceBy;
            positiveResponce = positive_Responce;
            ResponceMessage = Responce_Message;
            isThereResponses = true;
        }
    }
}
