using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorController
{
    class Door_Implementation : MarshalByRefObject, RemoteInerfaces.Services.IDeviceService
    {
        private bool isDeviceActive = true;
        public void changeDeviceState(bool isActive)
        {
            isDeviceActive = isActive;
            if (isActive)
            {
                Console.WriteLine("Device is Active");
            }
            else
            {
                Console.WriteLine("Device is Desactive");
            }
        }
    }
}
