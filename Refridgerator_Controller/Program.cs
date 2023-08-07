using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Refridgerator_Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(3035);
            ChannelServices.RegisterChannel(ch, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Refridgerator_Implementation), "Refridgerator", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Refridgerator Coontroller Server is Ready");
            Console.Read();
        }
    }
}
