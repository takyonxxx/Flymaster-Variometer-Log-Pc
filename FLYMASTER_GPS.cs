/* 
 * Project:    SerialPort Terminal
 * Company:    Coad .NET, http://coad.net
 * Author:     Noah Coad, http://coad.net/noah
 * Created:    March 2005
 * 
 * Notes:      This was created to demonstrate how to use the SerialPort control for
 *             communicating with your PC's Serial RS-232 COM Port
 * 
 *             It is for educational purposes only and not sanctified for industrial use. :)
 *             Written to support the blog post article at: http://msmvps.com/blogs/coad/archive/2005/03/23/39466.aspx
 * 
 *             Search for "comport" to see how I'm using the SerialPort control.
 */

#region Namespace Inclusions
using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Flymaster_Terminal.Properties;
using System.Threading;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Management; 

#endregion

namespace Flymaster_Terminal
{
    #region Public Enumerations
    public enum DataMode { Text, Hex }
    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };
    #endregion
   
    public partial class frmTerminal : Form
    {
        #region Local Variables
        private StringBuilder receiveBuffer = new StringBuilder();
        private byte[] bytesSEND = new byte[1] { 0xB1 };
        private byte[] bytesRESEND = new byte[1] { 0xB2 };
        private byte[] bytesABORT = new byte[1] { 0xB3 };
        private string cmdstring = string.Empty;
        private bool flightlist = false;
        private bool gpsdataflow = false;
        private string data = string.Empty;
        private SerialPort comport = new SerialPort();       
        private Settings settings = Settings.Default;
        double Latitude = 0, Longitude = 0, Altitude = 0, Presure = 0, Gpstime = 0, PresureAlt = 0;
        private string igc = string.Empty;
        private string flightview = string.Empty;
        private string flightdata = string.Empty;
        private string buff = string.Empty;
        private bool gettingdata = false;
        private int trackcount = 0;
        #endregion

        #region Constructor
        public frmTerminal()
        {
            // Load user settings
            settings.Reload();

            // Build the form
            InitializeComponent();

            // Restore the users settings
            InitializeControlValues();
            // Enable/disable controls based on the current state
            EnableControls();
            // When data is recieved through the port, call this method
            comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }


        #endregion

        #region Local Methods

        /// <summary> Save the user's settings. </summary>
        private void SaveSettings()
        {
            settings.BaudRate = int.Parse(cmbBaudRate.Text);
            settings.DataBits = int.Parse(cmbDataBits.Text);
            settings.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
            settings.PortName = cmbPortName.Text;
            settings.Save();
        }
        private string usbport = null;
        /// <summary> Populate the form's controls with default settings. </summary>
        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            cmbParity.Text = settings.Parity.ToString();
            cmbStopBits.Text = settings.StopBits.ToString();
            cmbDataBits.Text = settings.DataBits.ToString();
            cmbParity.Text = settings.Parity.ToString();
            cmbBaudRate.Text = settings.BaudRate.ToString();            
            
            foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
            {
                if (comPort.Description.StartsWith("USB Serial Port"))
                {
                    cmbPortName.Items.Add(comPort.Name);
                    cmbPortName.SelectedItem = comPort.Name;
                }
            }
           
        }

        /// <summary> Enable/disable controls based on the app's current state. </summary>
        private void EnableControls()
        {
            // Enable/disable controls based on whether the port is open or not
            //gbPortSettings.Enabled = !comport.IsOpen;
            rtfTerminal.Text = "";          
            btn_crtigc.Enabled = false;
            btn_downloadwp.Enabled = btn_uploadwp.Enabled =  btn_refresh.Enabled = listBox_flightlogs.Enabled = rtfTerminal.Enabled = comport.IsOpen;
            //chkDTR.Enabled = chkRTS.Enabled = comport.IsOpen;

            if (comport.IsOpen) btnOpenPort.Text = "Disconnect Flymaster";
            else btnOpenPort.Text = "Connect Flymaster";
        }

        #endregion

        #region Event Handlers
        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.bilisimbiliyor.com");
            //// Show the user the about dialog
            //(new frmAbout()).ShowDialog(this);
        }

        private void frmTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            gpsdataflow = false;
            //SendData("$PFMSNP,*3A");
            //SendData(" $PFMDNL,*1D");
            if (comport.IsOpen) comport.Close();
        }

        private void cmbBaudRate_Validating(object sender, CancelEventArgs e)
        { int x; e.Cancel = !int.TryParse(cmbBaudRate.Text, out x); }

        private void cmbDataBits_Validating(object sender, CancelEventArgs e)
        { int x; e.Cancel = !int.TryParse(cmbDataBits.Text, out x); }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            bool error = false;
            lbl_device.Text = string.Empty;
            // If the port is open, close it.
            if (comport.IsOpen) { 
                comport.Close();
                listBox_flightlogs.Items.Clear();
                rtfTerminal.Clear();              
                flightlist = false;
                gpsdataflow = false;            
            }
            else
            {                
                // Set the port's settings
                comport.BaudRate = int.Parse(cmbBaudRate.Text);
                comport.DataBits = int.Parse(cmbDataBits.Text);
                comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport.PortName = cmbPortName.Text;
                //comport.WriteTimeout = 100;
                comport.Handshake = Handshake.None;
                try
                {// Open the port
                    comport.Open();
                    if (comport.IsOpen)
                    {
                        SendData("$PFMSNP,*3A");
                        Thread.Sleep(100);
                        SendData("$PFMSNP,*3A");
                        Thread.Sleep(100);
                        //SendData("$PFMPLT,Turkay Biliyor,1,Ozone,M3,*cs"); 
                        listBox_flightlogs.Items.Clear();
                        SendData("$PFMDNL,LST,*56");
                        btn_enablegps.Enabled = true;                      
                    }
                }
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }
                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
               
            }
            EnableControls();
        }


        #endregion
        private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        {
            // checks to see if COM ports have been added or removed
            // since it is quite common now with USB-to-Serial adapters
            RefreshComPortList();           
            if (!lbl_device.Text.StartsWith("GPS"))
            {
                try
                {
                    if (comport.IsOpen)
                    {
                        comport.Close();
                        listBox_flightlogs.Items.Clear();
                        rtfTerminal.Clear();
                        receiveBuffer.Length = 0;
                        flightlist = false;
                        gpsdataflow = false;
                        EnableControls();
                        btn_enablegps.Enabled = false;
                        lbl_device.Text = "Flymaster not found! Check cable and com port...";
                    }
                }
                catch { }
            }
            
        }

        private void RefreshComPortList()
        {
            string selected;
            // Determain if the list of com port names has changed since last checked
            selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, comport.IsOpen);
            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }
        
        private string[] OrderedPortNames()
        {
            //// Just a placeholder for a successful parsing of a string to an integer
            int num;
            //// Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray(); 
        }
        
        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;
            
            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();
            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;
            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();                
                // Find newest port if one or more were added
                //string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();
                string newest = null;
                foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
                {
                    if (comPort.Description.StartsWith("USB Serial Port"))
                    {
                         newest = comPort.Name;
                    }
                }
                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rtfTerminal.Clear();
            receiveBuffer.Length = 0;
        }


        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
                //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));      
            }
            return sb.ToString().ToUpper();
        }

        private int HexToInt(string hex)
        {
            int myInt = Convert.ToInt32(hex, 16);
            if (hex.StartsWith("F") || hex.StartsWith("E") || hex.StartsWith("D") || hex.StartsWith("C") || hex.StartsWith("B") || hex.StartsWith("A"))
                return myInt - 256;
            else
                return myInt;

        }     
        public static string get_gps_date(double Gpstime)
        {
            TimeSpan TS = TimeSpan.FromSeconds(Gpstime);
            int years = (int)( TS.Days / 365.2425);
            int monthofyear = (int)(TS.Days / 30.4166666667)-(int)(years*12);
            int days = (int)(TS.Days)-((int)(years * 365.2425) + (int)(monthofyear * 30.4166666667));
            return years.ToString("00", CultureInfo.InvariantCulture) + "-" +
                (monthofyear + 1).ToString("00", CultureInfo.InvariantCulture) + "-" +
                days.ToString("00", CultureInfo.InvariantCulture) +" "+
                TS.Hours.ToString("00", CultureInfo.InvariantCulture) + ":" +
                TS.Minutes.ToString("00", CultureInfo.InvariantCulture) + ":" +
                TS.Seconds.ToString("00", CultureInfo.InvariantCulture);              
        }
       
        double _eQuatorialEarthRadius = 6378.1370D;
        double _d2r = (Math.PI / 180D);

        private double HaversineInKM(double lat1, double long1, double lat2, double long2)
        {
            double dlong = (long2 - long1) * _d2r;
            double dlat = (lat2 - lat1) * _d2r;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * _d2r) * Math.Cos(lat2 * _d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = _eQuatorialEarthRadius * c;
            return d;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            btn_savewp.Enabled = false;
            if (btn_enablegps.Text == "Enable GPS Data")
            {
                flightlist = false;
                rtfTerminal.Clear();
                listBox_flightlogs.SelectedItem = null;
                rtfTerminal.Focus();
                gpsdataflow = true;
                btn_enablegps.Text = "Disable GPS Data";
                btn_uploadwp.Enabled = btn_refresh.Enabled = btn_downloadwp.Enabled = listBox_flightlogs.Enabled = btnOpenPort.Enabled = btn_crtigc.Enabled = false;

                SendData("$PFMNAV,*2E");
                Thread.Sleep(50);
                SendData("$PFMNAV,*2E"); 
                lbl_status.Text = "GPS DATA FLOW ENABLED.";
            }
            else
            {
                gpsdataflow = false;
                btn_enablegps.Text = "Enable GPS Data";
                btn_uploadwp.Enabled = btn_downloadwp.Enabled = listBox_flightlogs.Enabled = btn_refresh.Enabled= btnOpenPort.Enabled = btn_exit.Enabled = true;

                SendData("$PFMSNP,*3A");
                SendData(" $PFMDNL,*1D");
                lbl_status.Text = "GPS DATA FLOW DISABLED.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {                      
                this.Close();            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flightlist = false;
            gpsdataflow = false;     
            listBox_flightlogs.Items.Clear();            
            SendData("$PFMDNL,LST,*56");
            Thread.Sleep(25);
            SendData("$PFMDNL,LST,*56");           
        }

        private void listaddflight(LogMsgType msgtype, string msg)
        {
            listBox_flightlogs.Invoke(new EventHandler(delegate
            {
                try
                {
                    string[] strArr = msg.Split('$');
                    string strTemp = strArr[1];
                    string[] lineArr = strTemp.Split(',');
                    listBox_flightlogs.Items.Add("$Flight No:" + lineArr[2].ToString() + "," + lineArr[3].ToString() + "," + lineArr[4].ToString() + "--Flight Duration:" + lineArr[5].Substring(0,8));
                }
                catch { }

            }));
        }
        private void listaddserial(LogMsgType msgtype, string msg)
        {
            lbl_device.Invoke(new EventHandler(delegate
            {
                try
                {
                    string[] strArr = data.Split('$');
                    string strTemp = strArr[1];
                    string[] lineArr = strTemp.Split(',');
                    lbl_device.Text = "GPS DEVICE:" + lineArr[1].ToString() + "," + lineArr[2].ToString() + "," + lineArr[3].ToString() + "," + lineArr[4].ToString();
                }
                catch { }
            }));
        }
       
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_savewp.Enabled = false;
                rtfTerminal.Clear();
                receiveBuffer.Length = 0;
                trackcount = 0;
                string[] strArr = listBox_flightlogs.SelectedItem.ToString().Split('$');
                string strTemp = strArr[1];
                string[] lineArr = strTemp.Split(',');
                cmdstring = "$PFMDNL," + lineArr[1].Substring(6, 2) + lineArr[1].Substring(3, 2) +
                    lineArr[1].Substring(0, 2) + lineArr[2].Substring(0, 2) +
                    lineArr[2].Substring(3, 2) + lineArr[2].Substring(6, 2) + ",*1D";
                progressBar1.Value = 0;
                flightlist = true;
                Thread.Sleep(25);
                SendData(cmdstring);
                Thread.Sleep(25);
                SendData(cmdstring);                
                listBox_flightlogs.Enabled = false;
                btn_enablegps.Enabled = false;
                btn_crtigc.Enabled = false;
                btn_refresh.Enabled = false;
                btnOpenPort.Enabled = false;                
            }
            catch { }
        }      
       
        private void SendData(string data)
        {
            comport.WriteLine(data);    
        }
        public static string get_gps_time(double Gpstime)
        {
           TimeSpan TS = TimeSpan.FromSeconds(Gpstime);
           return TS.Hours.ToString("00", CultureInfo.InvariantCulture) + 
                  TS.Minutes.ToString("00", CultureInfo.InvariantCulture) + 
                  TS.Seconds.ToString("00", CultureInfo.InvariantCulture);
        }

        public string ConvertDecimalToDegMinSecLat(double value)
        {
            double dLat = (double)value / (double)60000;
            int deg = (int)dLat;
            dLat = Math.Abs(dLat - deg);
            double min = (double)(dLat * 60);
            if (value < 0)
            {
                return (-1 * deg).ToString("00", CultureInfo.InvariantCulture) +
                    min.ToString("00.000S", CultureInfo.InvariantCulture).Substring(0, 2) +
                    min.ToString("00.000S", CultureInfo.InvariantCulture).Substring(3, 4);
            }
            else
            {
                return deg.ToString("00", CultureInfo.InvariantCulture) +
                    min.ToString("00.000N", CultureInfo.InvariantCulture).Substring(0, 2) +
                    min.ToString("00.000N", CultureInfo.InvariantCulture).Substring(3, 4);
            }

        }
        public string ConvertDecimalToDegMinSecLong(double value)
        {
            double dLat = (double)value / (double)60000;
            int deg = (int)dLat;
            dLat = Math.Abs(dLat - deg);
            double min = (double)(dLat * 60);
            if (value < 0)
            {
                return (-1 * deg).ToString("000", CultureInfo.InvariantCulture) +
                    min.ToString("00.000W", CultureInfo.InvariantCulture).Substring(0, 2) +
                    min.ToString("00.000W", CultureInfo.InvariantCulture).Substring(3, 4);               
            }
            else
            {
                return (deg).ToString("000", CultureInfo.InvariantCulture) +
                    min.ToString("00.000E", CultureInfo.InvariantCulture).Substring(0, 2) +
                    min.ToString("00.000E", CultureInfo.InvariantCulture).Substring(3, 4);     
            }
        }
     
        private void Log(LogMsgType msgtype, string msg)
        {
            rtfTerminal.Invoke(new EventHandler(delegate
            {
                try
                {
                   rtfTerminal.AppendText(msg);
                }
                catch { }
            }));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            btn_savewp.Enabled = false;
            // saveFileDialog1.InitialDirectory = "C:\\";
            saveFileDialog1.Filter = "Igc File (*.igc)|*.igc";           
            string[] strArr = listBox_flightlogs.SelectedItem.ToString().Split('$');
            string strTemp = strArr[1];
            string[] lineArr = strTemp.Split(',');
            cmdstring = lineArr[1].Substring(6, 2) + lineArr[1].Substring(3, 2) +
                lineArr[1].Substring(0, 2) + lineArr[2].Substring(0, 2) +
                lineArr[2].Substring(3, 2) + lineArr[2].Substring(6, 2);
            string flightdate = cmdstring.Substring(4, 2) +
                cmdstring.Substring(2, 2) +
                cmdstring.Substring(0, 2);
            saveFileDialog1.FileName = "Flight_" + flightdate + "_" + cmdstring.Substring(6, 6);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != "")
                {
                    using (System.IO.TextWriter writer = new System.IO.StreamWriter(saveFileDialog1.FileName))
                    {
                        writer.WriteLine("AXGD111 " + lbl_device.Text.Substring(11, lbl_device.Text.Length-11));
                        writer.WriteLine("HFDTE" + flightdate);
                        writer.WriteLine("HOPLTPILOT: " + txt_pilotname.Text);
                        writer.WriteLine("HOGTYGLIDERTYPE: " + txt_gliderid.Text);
                        writer.WriteLine("HODTM100GPSDATUM: WGS-84");
                        writer.WriteLine("HOCIDCOMPETITIONID:");
                        writer.WriteLine("HOCCLCOMPETITION CLASS: " + txt_gliderltf.Text);
                        writer.WriteLine("HOSITSite: None");
                        igc=igc.Remove(igc.Length - 1, 1);
                        writer.WriteLine(igc);
                        writer.WriteLine("LXGD Türkay Biliyor Igc Version 1.00");
                        writer.WriteLine("LXGD Downloaded " + DateTime.Now);                       
                    }
                }                          
            }
        }
       
        private void btn_downloadwp_Click(object sender, EventArgs e)
        {
            btn_savewp.Enabled = true;
            btn_crtigc.Enabled = false;
            flightlist = false;
            rtfTerminal.Clear();
            SendData("$PFMWPL,*3C");
            Thread.Sleep(25);
            SendData("$PFMWPL,*3C");   
        }

        private void btn_uploadwp_Click(object sender, EventArgs e)
        {
            btn_savewp.Enabled = false;
            btn_crtigc.Enabled = false;
            flightlist = false;
            int i = 0;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();           
            openFileDialog1.Filter = "WPT Files (.wpt)|*.wpt";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(openFileDialog1.FileName))
                {                   
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //SendData("$PFMWPR,39.98457,N,32.45755,E,500,ASDXXXX");
                        try
                        {
                            if (line != "")
                            {
                                string wpname = string.Empty;
                                Regex regex = new Regex(@",");
                                string[] substrings = regex.Split(line);                                
                                if (substrings[5].Length > 16)
                                    wpname = substrings[5].Substring(0, 16).Trim();
                                else
                                    wpname = substrings[5].Trim();
                                string command = "$PFMWPR" + "," + substrings[0].Trim() + "," + substrings[1].Trim() +
                                    "," + substrings[2].Trim() + "," + substrings[3].Trim() + "," + substrings[4].Trim() + "," + wpname;
                                SendData(command);                              
                                Thread.Sleep(50);
                                i++;
                                lbl_status.Text="Uploading Waypoints: " +i.ToString();
                                Application.DoEvents();
                            }
                        }
                        catch { MessageBox.Show("WPT File damaged"); }
                    }
                    rtfTerminal.Clear();
                    SendData("$PFMWPL,*3C");
                    Thread.Sleep(25);
                    SendData("$PFMWPL,*3C");
                    lbl_status.Text = i.ToString() + " Waypoints Uploaded To Flymaster";
                }              
            }
            
        }

        private void btn_savewp_Click(object sender, EventArgs e)
        {
            btn_crtigc.Enabled = false;
            saveFileDialog1.Filter = "WPT File (*.wpt)|*.wpt";
            saveFileDialog1.FileName = "WP_" + DateTime.Today.ToShortDateString().ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != "")
                {
                    using (System.IO.TextWriter writer = new System.IO.StreamWriter(saveFileDialog1.FileName))
                    {
                        for (int i = 0; i <= rtfTerminal.Lines.Length - 2; i++)
                        {
                            Regex regex = new Regex(@",");
                            string[] substrings = regex.Split(rtfTerminal.Lines[i]);
                            
                               double lat = Convert.ToDouble("0," + substrings[0].Substring(4,substrings[0].Length-4).ToString())*60;
                               string strlat=Convert.ToDouble(substrings[0].Substring(1, 2)).ToString("00", CultureInfo.InvariantCulture)
                                   + lat.ToString("00.000", CultureInfo.InvariantCulture);
                               double longt = Convert.ToDouble("0," + substrings[2].Substring(4, substrings[2].Length - 4).ToString()) * 60;
                               string strlongt=Convert.ToDouble(substrings[2].Substring(0, 3)).ToString("000", CultureInfo.InvariantCulture)
                                   + longt.ToString("00.000", CultureInfo.InvariantCulture);
                               string command = strlat + "," + substrings[1].Trim() +
                                   "," + strlongt + "," + substrings[3].Trim() + ","
                                   + substrings[4].Trim() + "," + substrings[5].Trim();
                               writer.WriteLine(command);
                          
                        }
                    }
                }
            }
        }
       
        private void format_log_data(object sender, EventArgs e)
        {
            if (!comport.IsOpen) return;
            flightdata = receiveBuffer.ToString();
            rtfTerminal.Clear();
            igc = string.Empty;
            trackcount = 0;
            flightview = string.Empty;
            if (flightdata.IndexOf("A3A3") != -1 && flightdata.IndexOf("A2A2") != -1 && flightdata.IndexOf("A1A1") != -1)
            {
                flightdata = flightdata.Remove(flightdata.Length - (flightdata.Length - flightdata.IndexOf("A3A3")), (flightdata.Length - flightdata.IndexOf("A3A3")));
                Regex regex = new Regex(@"A1A1");
                string[] substrings = regex.Split(flightdata);
                for (int ctr0 = 1; ctr0 < substrings.Length; ctr0++)
                {
                    try
                    {
                        Latitude = Convert.ToInt32((substrings[ctr0].Substring(10, 2) + substrings[ctr0].Substring(8, 2) + substrings[ctr0].Substring(6, 2) + substrings[ctr0].Substring(4, 2)), 16);
                        Longitude = Convert.ToInt32((substrings[ctr0].Substring(18, 2) + substrings[ctr0].Substring(16, 2) + substrings[ctr0].Substring(14, 2) + substrings[ctr0].Substring(12, 2)), 16);
                        Altitude = Convert.ToInt32((substrings[ctr0].Substring(22, 2) + substrings[ctr0].Substring(20, 2)), 16);
                        Presure = Convert.ToInt32((substrings[ctr0].Substring(26, 2) + substrings[ctr0].Substring(24, 2)), 16);
                        Gpstime = Convert.ToInt32((substrings[ctr0].Substring(34, 2) + substrings[ctr0].Substring(32, 2) + substrings[ctr0].Substring(30, 2) + substrings[ctr0].Substring(28, 2)), 16);
                        PresureAlt = Convert.ToInt32((double)(1 - (double)Math.Pow(Math.Abs((Presure / 10.0) / 1013.25), 0.190284)) * 44307.69);
                        string strLatitude = ConvertDecimalToDegMinSecLat(Latitude);
                        string strLongitude = ConvertDecimalToDegMinSecLong(Longitude);
                        flightview = "Time:" + get_gps_date(Gpstime) +
                            " Latitude:" + strLatitude +
                            " Longitude:" + strLongitude +
                            " PresureAlt:" + PresureAlt.ToString("00000", CultureInfo.InvariantCulture) +
                            " GPSAltitude:" + Altitude.ToString("00000", CultureInfo.InvariantCulture) +
                            "\r\n";
                        igc = igc + "B" + get_gps_time(Gpstime) + strLatitude + strLongitude + "A" +
                        PresureAlt.ToString("00000", CultureInfo.InvariantCulture) +
                        Altitude.ToString("00000", CultureInfo.InvariantCulture) + "\r\n";
                        Log(LogMsgType.Incoming, flightview);
                        trackcount++;
                        this.Invoke(new EventHandler(data_flow));
                        Application.DoEvents();
                    }
                    catch { }
                    Regex regexlog = new Regex(@"A2A2");
                    string[] substringslog = regexlog.Split(substrings[ctr0]);
                    for (int ctr1 = 1; ctr1 < substringslog.Length; ctr1++)
                    {
                        try
                        {
                            int loglat = 4;
                            int loglong = 6;
                            int logalt = 8;
                            int presure = 10;
                            int logtime = 12;
                            int i = 1;
                            for (i = 1; i <= substringslog[ctr1].Length; i++)
                            {
                                Latitude = Latitude + HexToInt(substringslog[ctr1].Substring(loglat, 2));
                                Longitude = Longitude + HexToInt(substringslog[ctr1].Substring(loglong, 2));
                                Altitude = Altitude + HexToInt(substringslog[ctr1].Substring(logalt, 2));
                                Presure = Presure + HexToInt(substringslog[ctr1].Substring(presure, 2));
                                Gpstime = Gpstime + HexToInt(substringslog[ctr1].Substring(logtime, 2));
                                PresureAlt = Convert.ToInt32((double)(1 - (double)Math.Pow(Math.Abs((Presure / 10.0) / 1013.25), 0.190284)) * 44307.69);
                                string strLatitude = ConvertDecimalToDegMinSecLat(Latitude);
                                string strLongitude = ConvertDecimalToDegMinSecLong(Longitude);
                                flightview = "Time:" + get_gps_date(Gpstime) +
                                    " Latitude:" + strLatitude +
                                    " Longitude:" + strLongitude +
                                    " PresureAlt:" + PresureAlt.ToString("00000", CultureInfo.InvariantCulture) +
                                    " GPSAltitude:" + Altitude.ToString("00000", CultureInfo.InvariantCulture) +
                                    "\r\n";
                                igc = igc + "B" + get_gps_time(Gpstime) + strLatitude + strLongitude + "A" +
                                PresureAlt.ToString("00000", CultureInfo.InvariantCulture) +
                                Altitude.ToString("00000", CultureInfo.InvariantCulture) + "\r\n";
                                loglat = loglat + 12;
                                loglong = loglong + 12;
                                logalt = logalt + 12;
                                presure = presure + 12;
                                logtime = logtime + 12;
                                Log(LogMsgType.Incoming, flightview);
                                trackcount++;
                                this.Invoke(new EventHandler(data_flow));
                                Application.DoEvents();
                            }
                        }
                        catch { }
                    }
                }
                listBox_flightlogs.Enabled = true;
                btn_enablegps.Enabled = true;
                btn_crtigc.Enabled = true;
                btn_refresh.Enabled = true;
                btnOpenPort.Enabled = true;
            }
            else
            {
                rtfTerminal.Clear();
                rtfTerminal.Text = "DATA NOT READY OR CORRUPTED...!\n";
                flightlist = false;
                listBox_flightlogs.Enabled = true;
            }
        }
       
        private void data_flow(object sender, EventArgs e)
        {
            if (!gettingdata)
            {
                progressBar1.Maximum = trackcount;
                progressBar1.Value = trackcount;
                lbl_status.Text = "Tracks: " + trackcount.ToString();
            }else
                lbl_status.Text = "Preparing data. Please wait...";

        }        
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!comport.IsOpen) return;
            SerialPort sp = (SerialPort)sender;
            try
            {
                while (sp.BytesToRead >0)
                {
                    if (!flightlist)
                    {
                        data = sp.ReadLine().ToString();
                        if (data.StartsWith("$PFMSNP"))
                            listaddserial(LogMsgType.Incoming, data);
                        else if (data.StartsWith("$PFMLST"))
                            listaddflight(LogMsgType.Incoming, data);
                        else if (!data.StartsWith("$PFMWPR"))
                        {
                            if (!gpsdataflow)
                                Log(LogMsgType.Incoming, data.Substring(8, data.Length - 8).ToString().Remove(42) + "\n");
                            else
                                Log(LogMsgType.Incoming, data);
                        }

                    }
                    else
                    {                        
                        int bytes = sp.BytesToRead;
                        byte[] buffer = new byte[bytes];
                        sp.Read(buffer, 0, bytes);
                        sp.Write(bytesSEND, 0, 1);
                        buff = ByteArrayToHexString(buffer).ToString();
                        receiveBuffer.Append(buff);
                        if (!gettingdata)
                        {
                            gettingdata = true;
                            this.Invoke(new EventHandler(data_flow));
                        }
                        if (receiveBuffer.ToString().IndexOf("A3A3") != -1)
                        {
                            gettingdata = false;
                            this.Invoke(new EventHandler(format_log_data));
                        }
                    }
                }
            }
            catch { }
        }
        
        internal class ProcessConnection
        {
            public static ConnectionOptions ProcessConnectionOptions()
            {
                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = ImpersonationLevel.Impersonate;
                options.Authentication = AuthenticationLevel.Default;
                options.EnablePrivileges = true;
                return options;
            }
            public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
            {
                ManagementScope connectScope = new ManagementScope();
                connectScope.Path = new ManagementPath(@"\\" + machineName + path);
                connectScope.Options = options;
                connectScope.Connect();
                return connectScope;
            }
        }
        public class COMPortInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public COMPortInfo() { }
            public static List<COMPortInfo> GetCOMPortsInfo()
            {
                List<COMPortInfo> comPortInfoList = new List<COMPortInfo>();
                ConnectionOptions options = ProcessConnection.ProcessConnectionOptions();
                ManagementScope connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");
                ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
                ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);
                using (comPortSearcher)
                {
                    string caption = null;
                    foreach (ManagementObject obj in comPortSearcher.Get())
                    {
                        if (obj != null)
                        {
                            object captionObj = obj["Caption"];
                            if (captionObj != null)
                            {
                                caption = captionObj.ToString();
                                if (caption.Contains("(COM"))
                                {
                                    COMPortInfo comPortInfo = new COMPortInfo();
                                    comPortInfo.Name = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", string.Empty).Replace(")",string.Empty);
                                    comPortInfo.Description = caption;
                                    comPortInfoList.Add(comPortInfo);
                                }
                            }
                        }
                    }
                }
                return comPortInfoList;
            }
        }

        private void frmTerminal_Load(object sender, EventArgs e)
        {
            cmbBaudRate.SelectedIndex = 6;
        }

     }
}