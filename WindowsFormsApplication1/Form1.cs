using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithm;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Connector connector;
        
        public Form1()
        {
            
            InitializeComponent();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            // Initialize a new Connector and add event handlers
            connector = new Connector();

            connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            connector.DeviceFound += new EventHandler(OnDeviceFound);
            connector.DeviceConnectFail += new EventHandler(OnDeviceFail);

            connector.DeviceDisconnected += new EventHandler(OnDeviceDisconnected);
            // Scan for devices across COM ports
            // The COM port named will be the first COM port that is checked.
            //string COM12 = null;
            
            connector.ConnectScan("COM31");
            //Thread.Sleep(45000);
            if (true)
            {
                progressBar2.Value = 0;
                Thread.Sleep(2000);
                progressBar2.Value = 20;
                Thread.Sleep(2000);
                progressBar2.Value = 40;
                Thread.Sleep(2000);
                progressBar2.Value = 60;
                Thread.Sleep(2000);
                progressBar2.Value = 80;
                Thread.Sleep(2000);
                progressBar2.Value = 100;
            }

            
        }

        
        
        private void OnDeviceFound(object sender, EventArgs e)
        {
            Console.WriteLine("Device Found!:");
            //Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;
            //MessageBox.Show("Device found: " + de.Device.PortName, "Connect");
            
        }

        private void OnDeviceFail(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            //MessageBox.Show("No devices found!");
            pictureBox1.Image = Properties.Resources.nosignal_v1;
        }

        private void OnDeviceConnected(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.connected_v1;
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;
            Console.WriteLine("Device found: " + de.Device.PortName, "Connect");
            
            de.Device.DataReceived += new EventHandler(OnDataReceived);
        }

        private void OnDataReceived(object sender, EventArgs e)
        {
            Device d = (Device)sender;
            TextBox lb_value = new TextBox();
            lb_value.Text = "datarecieved";
            Device.DataEventArgs de = (Device.DataEventArgs)e;
            NeuroSky.ThinkGear.DataRow[] tempDataRowArray = de.DataRowArray;

            TGParser tgParser = new TGParser();
            tgParser.Read(de.DataRowArray);

            //bool hasBlink = false;
            //int blinkeye = 0;

            /* Loops through the newly parsed data of the connected headset*/
            // The comments below indicate and can be used to print out the different data outputs. 

            //            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            //            {
            //                if (radio_att.Checked == true & tgParser.ParsedData[i].ContainsKey("Attention")
            //                    )
            //                {
            //
            //                    updateAttentionMode(tgParser, i);
            //
            //                }
            //                else if (radio_me.Checked == true & tgParser.ParsedData[i].ContainsKey("Meditation")
            //                    )
            //                {
            //                    updateMediationMode(tgParser, i);
            //                }
            //
            //             }

            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            {
                if (tgParser.ParsedData[i].ContainsKey("Attention"))
                {

                    lb_value.AppendText("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                    Console.WriteLine("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("Meditation"))
                {

                    lb_value.AppendText("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                    Console.WriteLine("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                }
            }
        }

        private void updateMediationMode(TGParser tgParser, int i)
        {
            if (tgParser.ParsedData[i]["Meditation"] >= 10)
            {
//this.progressBar2.Value = 100;
                Console.WriteLine("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                lb_value.Text = tgParser.ParsedData[i]["Meditation"].ToString();
                
                System.Media.SystemSounds.Asterisk.Play();
//                progressBar2.Visible = true;

                //pictureBox1.Image = Properties.Resources.on;
            }
            else
            {
                //pictureBox1.Image = Properties.Resources.off;
            }
        }
        
        private void updateAttentionMode(TGParser tgParser, int i)
        {
            if (tgParser.ParsedData[i]["Attention"] >= 50)
            {
                Console.WriteLine("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                lb_value.Text = tgParser.ParsedData[i]["Attention"].ToString();
                progressBar2.Value = Convert.ToInt32(tgParser.ParsedData[i]["Attention"]);

                progressBar2.Visible = true;
                
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            connector.Close();
            progressBar2.Value = 0;
        }

        private void OnDeviceDisconnected(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.nosignal_v1;
            System.Media.SystemSounds.Beep.Play();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.nosignal_v1;
        }

        private void Form1_UnLoad(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
