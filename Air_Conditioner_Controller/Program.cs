using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Air_Conditioner_Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(3034);
            ChannelServices.RegisterChannel(ch, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Air_Conditioner_Implementation), "Air_Conditioner", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Air_Conditioner Coontroller Server is Ready");
            Console.Read();
        }
    }
}
