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
using NeuroSky.ThinkGear.Algorithms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        Connector connector;
        public Form2()
        {
            connector = new Connector();

            connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            connector.Connect("COM31");
            InitializeComponent();
        }

        

        private void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;
        }

        private void OnDataReceived(object sender, EventArgs e)
        {
            Device d = (Device)sender;
            TextBox lm_value = new TextBox();
            lm_value.Text = "datarecieved";
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

                    lm_value.AppendText("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                    Equals("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("Meditation"))
                {

                    lm_value.AppendText("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                    Equals("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                }
            }
        }
    }
}
