using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace TemperaturHumiditySensorCoontroller
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(8081);
            ChannelServices.RegisterChannel(ch,false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Temperature_Humidity_Imp), "Temperature_Humidity", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Temperatur_Humidity Sensor Coontroller Server is Ready");
            Console.Read();
        }
    }
}
