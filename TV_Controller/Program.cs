using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace TV_Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(3030);
            ChannelServices.RegisterChannel(ch, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(TV_Implementation), "TV", WellKnownObjectMode.SingleCall);
            Console.WriteLine("TV Coontroller Server is Ready");
            Console.Read();
        }
    }
}
