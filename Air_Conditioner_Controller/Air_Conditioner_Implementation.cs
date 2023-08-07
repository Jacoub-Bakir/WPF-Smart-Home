using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Conditioner_Controller
{
    class Air_Conditioner_Implementation : MarshalByRefObject, RemoteInerfaces.Services.IDeviceService
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
