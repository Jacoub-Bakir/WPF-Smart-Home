using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace CameraController
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(3032);
            ChannelServices.RegisterChannel(ch, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Camera_Implementation), "Camera", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Camera Coontroller Server is Ready");
            Console.Read();
        }
    }
}
