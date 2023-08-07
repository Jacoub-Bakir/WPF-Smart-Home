using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace DoorController
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(3036);
            ChannelServices.RegisterChannel(ch, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Door_Implementation), "Door", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Door Coontroller Server is Ready");
            Console.Read();
        }
    }
}
