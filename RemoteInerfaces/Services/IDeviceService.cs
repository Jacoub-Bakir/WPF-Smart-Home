using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services
{
    public interface IDeviceService
    {
        void changeDeviceState(bool isActive);
    }
}
