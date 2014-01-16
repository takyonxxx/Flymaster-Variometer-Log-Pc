namespace Flymaster_Terminal
{
  partial class frmTerminal
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.cmbPortName = new System.Windows.Forms.ComboBox();
        this.cmbBaudRate = new System.Windows.Forms.ComboBox();
        this.lblComPort = new System.Windows.Forms.Label();
        this.lblBaudRate = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.cmbParity = new System.Windows.Forms.ComboBox();
        this.lblDataBits = new System.Windows.Forms.Label();
        this.cmbDataBits = new System.Windows.Forms.ComboBox();
        this.lblStopBits = new System.Windows.Forms.Label();
        this.cmbStopBits = new System.Windows.Forms.ComboBox();
        this.btnOpenPort = new System.Windows.Forms.Button();
        this.gbPortSettings = new System.Windows.Forms.GroupBox();
        this.btn_savewp = new System.Windows.Forms.Button();
        this.btn_uploadwp = new System.Windows.Forms.Button();
        this.btn_downloadwp = new System.Windows.Forms.Button();
        this.txt_gliderltf = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.txt_gliderid = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.txt_glidermodel = new System.Windows.Forms.TextBox();
        this.txt_pilotname = new System.Windows.Forms.TextBox();
        this.btn_enablegps = new System.Windows.Forms.Button();
        this.btn_crtigc = new System.Windows.Forms.Button();
        this.lnkAbout = new System.Windows.Forms.LinkLabel();
        this.btn_exit = new System.Windows.Forms.Button();
        this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.listBox_flightlogs = new System.Windows.Forms.ListBox();
        this.lbl_device = new System.Windows.Forms.Label();
        this.lbl_logs = new System.Windows.Forms.Label();
        this.rtfTerminal = new System.Windows.Forms.RichTextBox();
        this.btn_refresh = new System.Windows.Forms.Button();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.lbl_status = new System.Windows.Forms.Label();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.progressBar1 = new System.Windows.Forms.ProgressBar();
        this.gbPortSettings.SuspendLayout();
        this.SuspendLayout();
        // 
        // cmbPortName
        // 
        this.cmbPortName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbPortName.FormattingEnabled = true;
        this.cmbPortName.Location = new System.Drawing.Point(13, 35);
        this.cmbPortName.Name = "cmbPortName";
        this.cmbPortName.Size = new System.Drawing.Size(67, 21);
        this.cmbPortName.TabIndex = 1;
        // 
        // cmbBaudRate
        // 
        this.cmbBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cmbBaudRate.FormattingEnabled = true;
        this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
        this.cmbBaudRate.Location = new System.Drawing.Point(86, 35);
        this.cmbBaudRate.Name = "cmbBaudRate";
        this.cmbBaudRate.Size = new System.Drawing.Size(69, 21);
        this.cmbBaudRate.TabIndex = 3;
        this.cmbBaudRate.Validating += new System.ComponentModel.CancelEventHandler(this.cmbBaudRate_Validating);
        // 
        // lblComPort
        // 
        this.lblComPort.AutoSize = true;
        this.lblComPort.Location = new System.Drawing.Point(12, 19);
        this.lblComPort.Name = "lblComPort";
        this.lblComPort.Size = new System.Drawing.Size(65, 13);
        this.lblComPort.TabIndex = 0;
        this.lblComPort.Text = "COM Port:";
        // 
        // lblBaudRate
        // 
        this.lblBaudRate.AutoSize = true;
        this.lblBaudRate.Location = new System.Drawing.Point(85, 19);
        this.lblBaudRate.Name = "lblBaudRate";
        this.lblBaudRate.Size = new System.Drawing.Size(71, 13);
        this.lblBaudRate.TabIndex = 2;
        this.lblBaudRate.Text = "Baud Rate:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(163, 19);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(43, 13);
        this.label1.TabIndex = 4;
        this.label1.Text = "Parity:";
        // 
        // cmbParity
        // 
        this.cmbParity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbParity.FormattingEnabled = true;
        this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
        this.cmbParity.Location = new System.Drawing.Point(161, 35);
        this.cmbParity.Name = "cmbParity";
        this.cmbParity.Size = new System.Drawing.Size(60, 21);
        this.cmbParity.TabIndex = 5;
        // 
        // lblDataBits
        // 
        this.lblDataBits.AutoSize = true;
        this.lblDataBits.Location = new System.Drawing.Point(229, 19);
        this.lblDataBits.Name = "lblDataBits";
        this.lblDataBits.Size = new System.Drawing.Size(63, 13);
        this.lblDataBits.TabIndex = 6;
        this.lblDataBits.Text = "Data Bits:";
        // 
        // cmbDataBits
        // 
        this.cmbDataBits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cmbDataBits.FormattingEnabled = true;
        this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
        this.cmbDataBits.Location = new System.Drawing.Point(227, 35);
        this.cmbDataBits.Name = "cmbDataBits";
        this.cmbDataBits.Size = new System.Drawing.Size(60, 21);
        this.cmbDataBits.TabIndex = 7;
        this.cmbDataBits.Validating += new System.ComponentModel.CancelEventHandler(this.cmbDataBits_Validating);
        // 
        // lblStopBits
        // 
        this.lblStopBits.AutoSize = true;
        this.lblStopBits.Location = new System.Drawing.Point(295, 19);
        this.lblStopBits.Name = "lblStopBits";
        this.lblStopBits.Size = new System.Drawing.Size(62, 13);
        this.lblStopBits.TabIndex = 8;
        this.lblStopBits.Text = "Stop Bits:";
        // 
        // cmbStopBits
        // 
        this.cmbStopBits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbStopBits.FormattingEnabled = true;
        this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
        this.cmbStopBits.Location = new System.Drawing.Point(293, 35);
        this.cmbStopBits.Name = "cmbStopBits";
        this.cmbStopBits.Size = new System.Drawing.Size(65, 21);
        this.cmbStopBits.TabIndex = 9;
        // 
        // btnOpenPort
        // 
        this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnOpenPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.btnOpenPort.Location = new System.Drawing.Point(13, 62);
        this.btnOpenPort.Name = "btnOpenPort";
        this.btnOpenPort.Size = new System.Drawing.Size(169, 34);
        this.btnOpenPort.TabIndex = 6;
        this.btnOpenPort.Text = "Connect Flymaster";
        this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
        // 
        // gbPortSettings
        // 
        this.gbPortSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.gbPortSettings.Controls.Add(this.btn_savewp);
        this.gbPortSettings.Controls.Add(this.btn_uploadwp);
        this.gbPortSettings.Controls.Add(this.btn_downloadwp);
        this.gbPortSettings.Controls.Add(this.txt_gliderltf);
        this.gbPortSettings.Controls.Add(this.label6);
        this.gbPortSettings.Controls.Add(this.txt_gliderid);
        this.gbPortSettings.Controls.Add(this.label5);
        this.gbPortSettings.Controls.Add(this.label4);
        this.gbPortSettings.Controls.Add(this.txt_glidermodel);
        this.gbPortSettings.Controls.Add(this.txt_pilotname);
        this.gbPortSettings.Controls.Add(this.btn_enablegps);
        this.gbPortSettings.Controls.Add(this.cmbPortName);
        this.gbPortSettings.Controls.Add(this.btn_crtigc);
        this.gbPortSettings.Controls.Add(this.cmbBaudRate);
        this.gbPortSettings.Controls.Add(this.btnOpenPort);
        this.gbPortSettings.Controls.Add(this.lnkAbout);
        this.gbPortSettings.Controls.Add(this.cmbStopBits);
        this.gbPortSettings.Controls.Add(this.cmbParity);
        this.gbPortSettings.Controls.Add(this.cmbDataBits);
        this.gbPortSettings.Controls.Add(this.lblComPort);
        this.gbPortSettings.Controls.Add(this.lblStopBits);
        this.gbPortSettings.Controls.Add(this.lblBaudRate);
        this.gbPortSettings.Controls.Add(this.lblDataBits);
        this.gbPortSettings.Controls.Add(this.label1);
        this.gbPortSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.gbPortSettings.Location = new System.Drawing.Point(12, 334);
        this.gbPortSettings.Name = "gbPortSettings";
        this.gbPortSettings.Size = new System.Drawing.Size(672, 155);
        this.gbPortSettings.TabIndex = 4;
        this.gbPortSettings.TabStop = false;
        this.gbPortSettings.Text = "Türkay Biliyor Flymaster Terminal";
        // 
        // btn_savewp
        // 
        this.btn_savewp.Enabled = false;
        this.btn_savewp.Location = new System.Drawing.Point(144, 107);
        this.btn_savewp.Name = "btn_savewp";
        this.btn_savewp.Size = new System.Drawing.Size(99, 23);
        this.btn_savewp.TabIndex = 24;
        this.btn_savewp.Text = "Save WP";
        this.btn_savewp.UseVisualStyleBackColor = true;
        this.btn_savewp.Click += new System.EventHandler(this.btn_savewp_Click);
        // 
        // btn_uploadwp
        // 
        this.btn_uploadwp.Location = new System.Drawing.Point(252, 107);
        this.btn_uploadwp.Name = "btn_uploadwp";
        this.btn_uploadwp.Size = new System.Drawing.Size(105, 23);
        this.btn_uploadwp.TabIndex = 23;
        this.btn_uploadwp.Text = "Upload WP";
        this.btn_uploadwp.UseVisualStyleBackColor = true;
        this.btn_uploadwp.Click += new System.EventHandler(this.btn_uploadwp_Click);
        // 
        // btn_downloadwp
        // 
        this.btn_downloadwp.Location = new System.Drawing.Point(13, 107);
        this.btn_downloadwp.Name = "btn_downloadwp";
        this.btn_downloadwp.Size = new System.Drawing.Size(119, 23);
        this.btn_downloadwp.TabIndex = 21;
        this.btn_downloadwp.Text = "Download WP";
        this.btn_downloadwp.UseVisualStyleBackColor = true;
        this.btn_downloadwp.Click += new System.EventHandler(this.btn_downloadwp_Click);
        // 
        // txt_gliderltf
        // 
        this.txt_gliderltf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.txt_gliderltf.Location = new System.Drawing.Point(592, 69);
        this.txt_gliderltf.Name = "txt_gliderltf";
        this.txt_gliderltf.Size = new System.Drawing.Size(73, 20);
        this.txt_gliderltf.TabIndex = 20;
        // 
        // label6
        // 
        this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(423, 72);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(85, 13);
        this.label6.TabIndex = 19;
        this.label6.Text = "Glider ID/LTF";
        // 
        // txt_gliderid
        // 
        this.txt_gliderid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.txt_gliderid.Location = new System.Drawing.Point(514, 69);
        this.txt_gliderid.Name = "txt_gliderid";
        this.txt_gliderid.Size = new System.Drawing.Size(73, 20);
        this.txt_gliderid.TabIndex = 18;
        // 
        // label5
        // 
        this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(423, 47);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(78, 13);
        this.label5.TabIndex = 17;
        this.label5.Text = "Glider Model";
        // 
        // label4
        // 
        this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(423, 22);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(68, 13);
        this.label4.TabIndex = 16;
        this.label4.Text = "Pilot Name";
        // 
        // txt_glidermodel
        // 
        this.txt_glidermodel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.txt_glidermodel.Location = new System.Drawing.Point(515, 44);
        this.txt_glidermodel.Name = "txt_glidermodel";
        this.txt_glidermodel.Size = new System.Drawing.Size(150, 20);
        this.txt_glidermodel.TabIndex = 15;
        // 
        // txt_pilotname
        // 
        this.txt_pilotname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.txt_pilotname.Location = new System.Drawing.Point(515, 19);
        this.txt_pilotname.Name = "txt_pilotname";
        this.txt_pilotname.Size = new System.Drawing.Size(150, 20);
        this.txt_pilotname.TabIndex = 14;
        // 
        // btn_enablegps
        // 
        this.btn_enablegps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btn_enablegps.Enabled = false;
        this.btn_enablegps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.btn_enablegps.Location = new System.Drawing.Point(189, 62);
        this.btn_enablegps.Name = "btn_enablegps";
        this.btn_enablegps.Size = new System.Drawing.Size(169, 34);
        this.btn_enablegps.TabIndex = 13;
        this.btn_enablegps.Text = "Enable GPS Data";
        this.btn_enablegps.Click += new System.EventHandler(this.button1_Click_1);
        // 
        // btn_crtigc
        // 
        this.btn_crtigc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btn_crtigc.BackColor = System.Drawing.Color.Green;
        this.btn_crtigc.Enabled = false;
        this.btn_crtigc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.btn_crtigc.ForeColor = System.Drawing.Color.White;
        this.btn_crtigc.Location = new System.Drawing.Point(514, 100);
        this.btn_crtigc.Name = "btn_crtigc";
        this.btn_crtigc.Size = new System.Drawing.Size(151, 34);
        this.btn_crtigc.TabIndex = 11;
        this.btn_crtigc.Text = "Create IGC";
        this.btn_crtigc.UseVisualStyleBackColor = false;
        this.btn_crtigc.Click += new System.EventHandler(this.button2_Click);
        // 
        // lnkAbout
        // 
        this.lnkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lnkAbout.AutoSize = true;
        this.lnkAbout.Location = new System.Drawing.Point(10, 137);
        this.lnkAbout.Name = "lnkAbout";
        this.lnkAbout.Size = new System.Drawing.Size(235, 13);
        this.lnkAbout.TabIndex = 8;
        this.lnkAbout.TabStop = true;
        this.lnkAbout.Text = "&By Türkay Biliyor www.bilisimbiliyor.com ";
        this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
        // 
        // btn_exit
        // 
        this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.btn_exit.Location = new System.Drawing.Point(576, 22);
        this.btn_exit.Name = "btn_exit";
        this.btn_exit.Size = new System.Drawing.Size(101, 28);
        this.btn_exit.TabIndex = 12;
        this.btn_exit.Text = "&EXIT";
        this.btn_exit.Click += new System.EventHandler(this.button3_Click);
        // 
        // tmrCheckComPorts
        // 
        this.tmrCheckComPorts.Enabled = true;
        this.tmrCheckComPorts.Interval = 500;
        this.tmrCheckComPorts.Tick += new System.EventHandler(this.tmrCheckComPorts_Tick);
        // 
        // listBox_flightlogs
        // 
        this.listBox_flightlogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.listBox_flightlogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.listBox_flightlogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.listBox_flightlogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.listBox_flightlogs.ForeColor = System.Drawing.Color.White;
        this.listBox_flightlogs.FormattingEnabled = true;
        this.listBox_flightlogs.ItemHeight = 20;
        this.listBox_flightlogs.Location = new System.Drawing.Point(12, 88);
        this.listBox_flightlogs.Name = "listBox_flightlogs";
        this.listBox_flightlogs.Size = new System.Drawing.Size(665, 122);
        this.listBox_flightlogs.TabIndex = 10;
        this.listBox_flightlogs.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
        // 
        // lbl_device
        // 
        this.lbl_device.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.lbl_device.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.lbl_device.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.lbl_device.ForeColor = System.Drawing.Color.White;
        this.lbl_device.Location = new System.Drawing.Point(12, 22);
        this.lbl_device.Name = "lbl_device";
        this.lbl_device.Size = new System.Drawing.Size(554, 31);
        this.lbl_device.TabIndex = 11;
        // 
        // lbl_logs
        // 
        this.lbl_logs.BackColor = System.Drawing.Color.Gray;
        this.lbl_logs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.lbl_logs.ForeColor = System.Drawing.Color.Yellow;
        this.lbl_logs.Location = new System.Drawing.Point(12, 60);
        this.lbl_logs.Name = "lbl_logs";
        this.lbl_logs.Size = new System.Drawing.Size(134, 25);
        this.lbl_logs.TabIndex = 12;
        this.lbl_logs.Text = "FLIGHT LOGS:";
        // 
        // rtfTerminal
        // 
        this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.rtfTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.rtfTerminal.Location = new System.Drawing.Point(12, 213);
        this.rtfTerminal.Name = "rtfTerminal";
        this.rtfTerminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
        this.rtfTerminal.Size = new System.Drawing.Size(665, 115);
        this.rtfTerminal.TabIndex = 13;
        this.rtfTerminal.Text = "";
        // 
        // btn_refresh
        // 
        this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.btn_refresh.Location = new System.Drawing.Point(576, 59);
        this.btn_refresh.Name = "btn_refresh";
        this.btn_refresh.Size = new System.Drawing.Size(101, 28);
        this.btn_refresh.TabIndex = 14;
        this.btn_refresh.Text = "&REFRESH";
        this.btn_refresh.Click += new System.EventHandler(this.button4_Click);
        // 
        // lbl_status
        // 
        this.lbl_status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
        this.lbl_status.ForeColor = System.Drawing.Color.White;
        this.lbl_status.Location = new System.Drawing.Point(152, 60);
        this.lbl_status.Name = "lbl_status";
        this.lbl_status.Size = new System.Drawing.Size(258, 25);
        this.lbl_status.TabIndex = 16;
        // 
        // openFileDialog1
        // 
        this.openFileDialog1.FileName = "openFileDialog1";
        // 
        // progressBar1
        // 
        this.progressBar1.Location = new System.Drawing.Point(416, 60);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new System.Drawing.Size(150, 23);
        this.progressBar1.Step = 1;
        this.progressBar1.TabIndex = 17;
        // 
        // frmTerminal
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(689, 493);
        this.Controls.Add(this.progressBar1);
        this.Controls.Add(this.lbl_status);
        this.Controls.Add(this.btn_refresh);
        this.Controls.Add(this.rtfTerminal);
        this.Controls.Add(this.lbl_logs);
        this.Controls.Add(this.lbl_device);
        this.Controls.Add(this.listBox_flightlogs);
        this.Controls.Add(this.btn_exit);
        this.Controls.Add(this.gbPortSettings);
        this.MinimumSize = new System.Drawing.Size(505, 250);
        this.Name = "frmTerminal";
        this.Text = "TÜRKAY BÝLÝYOR  FLYMASTER B1 NAV LIVE TERMINAL";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTerminal_FormClosing);
        this.Load += new System.EventHandler(this.frmTerminal_Load);
        this.gbPortSettings.ResumeLayout(false);
        this.gbPortSettings.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbPortName;
    private System.Windows.Forms.ComboBox cmbBaudRate;
    private System.Windows.Forms.Label lblComPort;
    private System.Windows.Forms.Label lblBaudRate;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbParity;
    private System.Windows.Forms.Label lblDataBits;
    private System.Windows.Forms.ComboBox cmbDataBits;
    private System.Windows.Forms.Label lblStopBits;
    private System.Windows.Forms.ComboBox cmbStopBits;
    private System.Windows.Forms.Button btnOpenPort;
    private System.Windows.Forms.GroupBox gbPortSettings;
    private System.Windows.Forms.LinkLabel lnkAbout;
		private System.Windows.Forms.Timer tmrCheckComPorts;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ListBox listBox_flightlogs;
        private System.Windows.Forms.Button btn_crtigc;
        private System.Windows.Forms.Label lbl_device;
        private System.Windows.Forms.Label lbl_logs;
        private System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_enablegps;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_glidermodel;
        private System.Windows.Forms.TextBox txt_pilotname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_gliderid;
        private System.Windows.Forms.TextBox txt_gliderltf;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Button btn_downloadwp;
        private System.Windows.Forms.Button btn_uploadwp;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_savewp;
        private System.Windows.Forms.ProgressBar progressBar1;
  }
}

