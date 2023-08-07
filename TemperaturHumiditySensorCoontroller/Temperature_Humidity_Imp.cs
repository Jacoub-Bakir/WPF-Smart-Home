
using System;
using System.Collections.Generic;
using System.Text;

namespace TemperaturHumiditySensorCoontroller
{
    class Temperature_Humidity_Imp : MarshalByRefObject, RemoteInerfaces.Temperatur_HumidityInterface, RemoteInerfaces.Services.IDeviceService
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

        public string getHumidity()
        {
            if (isDeviceActive)
            {
                Random rnd = new Random();
                int hum = rnd.Next(0, 100);
                return hum.ToString() + ".0 %";
            }
            return "";
        }

        public string getTemperature()
        {
            if (isDeviceActive)
            {
                Random rnd = new Random();
                int tem = rnd.Next(-50, 100);
                return tem.ToString() + ".0 °C";
            }
            return "";
        }
    }
}


