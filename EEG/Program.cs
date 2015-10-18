using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithm.EEGTools;
using NeuroSky.ThinkGear.Algorithm;
using NeuroSky.ThinkGear.Algorithms;
using NLog;
using NLog.ComInterop;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Filters;
using NLog.LayoutRenderers.Wrappers;
using NLog.Layouts;
using NLog.LogReceiverService;
using NLog.Targets.Wrappers;
using Jayrock.Collections;
using Jayrock.Configuration;
using Jayrock.Diagnostics;
using Jayrock.Json.Conversion.Converters;
using Jayrock.JsonML;
using Jayrock.Reflection;
using System.Threading;

namespace EEG
{
    class Program
    {
        static Connector connector;
        static byte poorSig;
        static void Main(string[] args)
        {
            Console.WriteLine("HelloEEG!");
            Connector connector = new Connector();
            connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            connector.DeviceConnectFail += new EventHandler(OnDeviceFail);
            connector.DeviceValidating += new EventHandler(OnDeviceValidating);
            connector.ConnectScan("COM13");
            Thread.Sleep(450000);
            System.Console.WriteLine("Goodbye.");
            connector.Close();
            Environment.Exit(0);
        }

        private static void OnDeviceValidating(object sender, EventArgs e)
        {
            Console.WriteLine("Validating: ");
        }

        private static void OnDeviceFail(object sender, EventArgs e)
        {
            Console.WriteLine("No devices found! :");
        }

        private static void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;

            Console.WriteLine("Device found on: " + de.Device.PortName);
            de.Device.DataReceived += new EventHandler(OnDataReceived);
        }

        private static void OnDataReceived(object sender, EventArgs e)
        {
            Device d = (Device)sender;

            Device.DataEventArgs de = (Device.DataEventArgs)e;
            DataRow[] tempDataRowArray = de.DataRowArray;

            TGParser tgParser = new TGParser();
            tgParser.Read(de.DataRowArray);

            /* Loops through the newly parsed data of the connected headset*/
            // The comments below indicate and can be used to print out the different data outputs. 

            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            {

                if (tgParser.ParsedData[i].ContainsKey("Raw"))
                {

                   // Console.WriteLine("Raw Value:" + tgParser.ParsedData[i]["Raw"]);

                }

                if (tgParser.ParsedData[i].ContainsKey("PoorSignal"))
                {

                    //The following line prints the Time associated with the parsed data
                    Console.WriteLine("Time:" + tgParser.ParsedData[i]["Time"]);

                    //A Poor Signal value of 0 indicates that your headset is fitting properly
                    Console.WriteLine("Poor Signal:" + tgParser.ParsedData[i]["PoorSignal"]);

                    poorSig = (byte)tgParser.ParsedData[i]["PoorSignal"];
                }


                if (tgParser.ParsedData[i].ContainsKey("Attention"))
                {

                    Console.WriteLine("Att Value:" + tgParser.ParsedData[i]["Attention"]);

                }


                if (tgParser.ParsedData[i].ContainsKey("Meditation"))
                {

                    Console.WriteLine("Med Value:" + tgParser.ParsedData[i]["Meditation"]);

                }


                if (tgParser.ParsedData[i].ContainsKey("EegPowerDelta"))
                {

                    Console.WriteLine("Delta: " + tgParser.ParsedData[i]["EegPowerDelta"]);

                }

                if (tgParser.ParsedData[i].ContainsKey("BlinkStrength"))
                {

                    //Console.WriteLine("Eyeblink " + tgParser.ParsedData[i]["BlinkStrength"]);

                }
            }
        }
    }
}
