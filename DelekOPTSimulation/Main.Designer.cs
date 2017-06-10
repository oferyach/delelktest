namespace DelekOPTSimulation
{
    partial class Main
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
            this.Exit = new System.Windows.Forms.Button();
            this.Self = new System.Windows.Forms.RadioButton();
            this.Full = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NoService = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CardTag = new System.Windows.Forms.Button();
            this.CardType = new System.Windows.Forms.ListBox();
            this.Pump = new System.Windows.Forms.GroupBox();
            this.Soler = new System.Windows.Forms.RadioButton();
            this.F95 = new System.Windows.Forms.RadioButton();
            this.PPV = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Money = new System.Windows.Forms.Label();
            this.Volume = new System.Windows.Forms.Label();
            this.Noz = new System.Windows.Forms.PictureBox();
            this.Validation = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentImage = new System.Windows.Forms.TextBox();
            this.FileVersion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.OtherMOP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MultiPump = new System.Windows.Forms.CheckBox();
            this.VIUAttn = new System.Windows.Forms.CheckBox();
            this.FuelLimit = new System.Windows.Forms.CheckBox();
            this.NeedDrop = new System.Windows.Forms.CheckBox();
            this.OverDropMoney = new System.Windows.Forms.CheckBox();
            this.DropWarn = new System.Windows.Forms.CheckBox();
            this.OpenTrans = new System.Windows.Forms.CheckBox();
            this.FuelingNow = new System.Windows.Forms.CheckBox();
            this.CommErrTR = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Pump.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Noz)).BeginInit();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(627, 727);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 0;
            this.Exit.Text = "יציאה";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Self
            // 
            this.Self.AutoSize = true;
            this.Self.Checked = true;
            this.Self.Location = new System.Drawing.Point(17, 21);
            this.Self.Name = "Self";
            this.Self.Size = new System.Drawing.Size(81, 17);
            this.Self.TabIndex = 24;
            this.Self.TabStop = true;
            this.Self.Text = "שרות עצמי";
            this.Self.UseVisualStyleBackColor = true;
            this.Self.CheckedChanged += new System.EventHandler(this.Self_CheckedChanged);
            // 
            // Full
            // 
            this.Full.AutoSize = true;
            this.Full.Location = new System.Drawing.Point(118, 21);
            this.Full.Name = "Full";
            this.Full.Size = new System.Drawing.Size(77, 17);
            this.Full.TabIndex = 25;
            this.Full.TabStop = true;
            this.Full.Text = "שרות מלא";
            this.Full.UseVisualStyleBackColor = true;
            this.Full.CheckedChanged += new System.EventHandler(this.Full_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NoService);
            this.groupBox1.Controls.Add(this.Self);
            this.groupBox1.Controls.Add(this.Full);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(339, 52);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "סוג שרות";
            // 
            // NoService
            // 
            this.NoService.AutoSize = true;
            this.NoService.Location = new System.Drawing.Point(222, 21);
            this.NoService.Name = "NoService";
            this.NoService.Size = new System.Drawing.Size(73, 17);
            this.NoService.TabIndex = 26;
            this.NoService.TabStop = true;
            this.NoService.Text = "אין שרות";
            this.NoService.UseVisualStyleBackColor = true;
            this.NoService.CheckedChanged += new System.EventHandler(this.NoService_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CardTag);
            this.groupBox2.Controls.Add(this.CardType);
            this.groupBox2.Location = new System.Drawing.Point(384, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(318, 146);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "הכנס כרטיס / הצג תג / דלקן";
            // 
            // CardTag
            // 
            this.CardTag.Location = new System.Drawing.Point(6, 109);
            this.CardTag.Name = "CardTag";
            this.CardTag.Size = new System.Drawing.Size(75, 23);
            this.CardTag.TabIndex = 37;
            this.CardTag.Text = "הצג / הכנס";
            this.CardTag.UseVisualStyleBackColor = true;
            this.CardTag.Click += new System.EventHandler(this.CardTag_Click);
            // 
            // CardType
            // 
            this.CardType.FormattingEnabled = true;
            this.CardType.Items.AddRange(new object[] {
            "אשראי",
            "מוכרן",
            "כב\"ת סולר",
            "כב\"ת + כל הפרומפט",
            "מנהל",
            "כרטיס פגום",
            "דלקן"});
            this.CardType.Location = new System.Drawing.Point(87, 19);
            this.CardType.Name = "CardType";
            this.CardType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CardType.Size = new System.Drawing.Size(226, 95);
            this.CardType.TabIndex = 36;
            // 
            // Pump
            // 
            this.Pump.Controls.Add(this.Soler);
            this.Pump.Controls.Add(this.F95);
            this.Pump.Controls.Add(this.PPV);
            this.Pump.Controls.Add(this.label10);
            this.Pump.Controls.Add(this.label9);
            this.Pump.Controls.Add(this.label8);
            this.Pump.Controls.Add(this.label7);
            this.Pump.Controls.Add(this.label6);
            this.Pump.Controls.Add(this.label5);
            this.Pump.Controls.Add(this.Money);
            this.Pump.Controls.Add(this.Volume);
            this.Pump.Controls.Add(this.Noz);
            this.Pump.Location = new System.Drawing.Point(384, 12);
            this.Pump.Name = "Pump";
            this.Pump.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Pump.Size = new System.Drawing.Size(318, 74);
            this.Pump.TabIndex = 31;
            this.Pump.TabStop = false;
            this.Pump.Text = "משאבה";
            // 
            // Soler
            // 
            this.Soler.AutoSize = true;
            this.Soler.Location = new System.Drawing.Point(-2, 46);
            this.Soler.Name = "Soler";
            this.Soler.Size = new System.Drawing.Size(50, 17);
            this.Soler.TabIndex = 39;
            this.Soler.TabStop = true;
            this.Soler.Text = "סולר";
            this.Soler.UseVisualStyleBackColor = true;
            // 
            // F95
            // 
            this.F95.AutoSize = true;
            this.F95.Checked = true;
            this.F95.Location = new System.Drawing.Point(11, 23);
            this.F95.Name = "F95";
            this.F95.Size = new System.Drawing.Size(37, 17);
            this.F95.TabIndex = 38;
            this.F95.TabStop = true;
            this.F95.Text = "95";
            this.F95.UseVisualStyleBackColor = true;
            // 
            // PPV
            // 
            this.PPV.AutoSize = true;
            this.PPV.Location = new System.Drawing.Point(271, 27);
            this.PPV.Name = "PPV";
            this.PPV.Size = new System.Drawing.Size(28, 13);
            this.PPV.TabIndex = 37;
            this.PPV.Text = "5.99";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(232, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(9, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "|";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(9, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "|";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(232, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(9, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "|";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(252, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "ש\"ח / ליטר";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "ש\"ח";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "ליטר";
            // 
            // Money
            // 
            this.Money.AutoSize = true;
            this.Money.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Money.Location = new System.Drawing.Point(108, 45);
            this.Money.Name = "Money";
            this.Money.Size = new System.Drawing.Size(87, 22);
            this.Money.TabIndex = 2;
            this.Money.Text = "0000.00";
            // 
            // Volume
            // 
            this.Volume.AutoSize = true;
            this.Volume.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Volume.Location = new System.Drawing.Point(108, 23);
            this.Volume.Name = "Volume";
            this.Volume.Size = new System.Drawing.Size(87, 22);
            this.Volume.TabIndex = 1;
            this.Volume.Text = "0000.00";
            // 
            // Noz
            // 
            this.Noz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Noz.Image = global::DelekOPTSimulation.Properties.Resources.PicNozzDown;
            this.Noz.Location = new System.Drawing.Point(55, 21);
            this.Noz.Name = "Noz";
            this.Noz.Size = new System.Drawing.Size(48, 43);
            this.Noz.TabIndex = 0;
            this.Noz.TabStop = false;
            this.Noz.Click += new System.EventHandler(this.Noz_Click);
            // 
            // Validation
            // 
            this.Validation.AutoSize = true;
            this.Validation.Location = new System.Drawing.Point(149, 86);
            this.Validation.Name = "Validation";
            this.Validation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Validation.Size = new System.Drawing.Size(203, 17);
            this.Validation.TabIndex = 32;
            this.Validation.Text = "הפעל בדיקות תקינות תעודת זהות  ";
            this.Validation.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(620, 105);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "תמונה נוכחית:";
            // 
            // CurrentImage
            // 
            this.CurrentImage.Location = new System.Drawing.Point(390, 102);
            this.CurrentImage.Name = "CurrentImage";
            this.CurrentImage.ReadOnly = true;
            this.CurrentImage.Size = new System.Drawing.Size(214, 20);
            this.CurrentImage.TabIndex = 34;
            // 
            // FileVersion
            // 
            this.FileVersion.AutoSize = true;
            this.FileVersion.Location = new System.Drawing.Point(10, 737);
            this.FileVersion.Name = "FileVersion";
            this.FileVersion.Size = new System.Drawing.Size(42, 13);
            this.FileVersion.TabIndex = 35;
            this.FileVersion.Text = "Version";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(9, 721);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(706, 1);
            this.panel1.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(544, 310);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "רשימת אמצאי אחר:";
            // 
            // OtherMOP
            // 
            this.OtherMOP.Location = new System.Drawing.Point(384, 307);
            this.OtherMOP.Name = "OtherMOP";
            this.OtherMOP.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OtherMOP.Size = new System.Drawing.Size(191, 20);
            this.OtherMOP.TabIndex = 38;
            this.OtherMOP.Text = "סלב, TMC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(666, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 39;
            // 
            // MultiPump
            // 
            this.MultiPump.AutoSize = true;
            this.MultiPump.Location = new System.Drawing.Point(189, 122);
            this.MultiPump.Name = "MultiPump";
            this.MultiPump.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MultiPump.Size = new System.Drawing.Size(163, 17);
            this.MultiPump.TabIndex = 40;
            this.MultiPump.Text = "שייוך ליותר ממשאבה אחת";
            this.MultiPump.UseVisualStyleBackColor = true;
            this.MultiPump.CheckedChanged += new System.EventHandler(this.MultiPump_CheckedChanged);
            // 
            // VIUAttn
            // 
            this.VIUAttn.AutoSize = true;
            this.VIUAttn.Location = new System.Drawing.Point(197, 150);
            this.VIUAttn.Name = "VIUAttn";
            this.VIUAttn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.VIUAttn.Size = new System.Drawing.Size(155, 17);
            this.VIUAttn.TabIndex = 41;
            this.VIUAttn.Text = "דלקן דורש כרטיס מתדלק";
            this.VIUAttn.UseVisualStyleBackColor = true;
            // 
            // FuelLimit
            // 
            this.FuelLimit.AutoSize = true;
            this.FuelLimit.Location = new System.Drawing.Point(237, 177);
            this.FuelLimit.Name = "FuelLimit";
            this.FuelLimit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FuelLimit.Size = new System.Drawing.Size(115, 17);
            this.FuelLimit.TabIndex = 42;
            this.FuelLimit.Text = "אפשר הגבלת נפח";
            this.FuelLimit.UseVisualStyleBackColor = true;
            // 
            // NeedDrop
            // 
            this.NeedDrop.AutoSize = true;
            this.NeedDrop.Location = new System.Drawing.Point(254, 211);
            this.NeedDrop.Name = "NeedDrop";
            this.NeedDrop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.NeedDrop.Size = new System.Drawing.Size(98, 17);
            this.NeedDrop.TabIndex = 43;
            this.NeedDrop.Text = "נדרשת הפקדה";
            this.NeedDrop.UseVisualStyleBackColor = true;
            // 
            // OverDropMoney
            // 
            this.OverDropMoney.AutoSize = true;
            this.OverDropMoney.Location = new System.Drawing.Point(126, 211);
            this.OverDropMoney.Name = "OverDropMoney";
            this.OverDropMoney.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OverDropMoney.Size = new System.Drawing.Size(125, 17);
            this.OverDropMoney.TabIndex = 44;
            this.OverDropMoney.Text = "הפקדה מעבר לקיים";
            this.OverDropMoney.UseVisualStyleBackColor = true;
            // 
            // DropWarn
            // 
            this.DropWarn.AutoSize = true;
            this.DropWarn.Location = new System.Drawing.Point(13, 211);
            this.DropWarn.Name = "DropWarn";
            this.DropWarn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DropWarn.Size = new System.Drawing.Size(99, 17);
            this.DropWarn.TabIndex = 45;
            this.DropWarn.Text = "התראת הפקדה";
            this.DropWarn.UseVisualStyleBackColor = true;
            // 
            // OpenTrans
            // 
            this.OpenTrans.AutoSize = true;
            this.OpenTrans.Location = new System.Drawing.Point(222, 244);
            this.OpenTrans.Name = "OpenTrans";
            this.OpenTrans.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OpenTrans.Size = new System.Drawing.Size(130, 17);
            this.OpenTrans.TabIndex = 46;
            this.OpenTrans.Text = "יש עיסקאות פתוחות";
            this.OpenTrans.UseVisualStyleBackColor = true;
            // 
            // FuelingNow
            // 
            this.FuelingNow.AutoSize = true;
            this.FuelingNow.Location = new System.Drawing.Point(88, 244);
            this.FuelingNow.Name = "FuelingNow";
            this.FuelingNow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FuelingNow.Size = new System.Drawing.Size(110, 17);
            this.FuelingNow.TabIndex = 47;
            this.FuelingNow.Text = "יש תילוקים כעת";
            this.FuelingNow.UseVisualStyleBackColor = true;
            // 
            // CommErrTR
            // 
            this.CommErrTR.AutoSize = true;
            this.CommErrTR.Location = new System.Drawing.Point(237, 278);
            this.CommErrTR.Name = "CommErrTR";
            this.CommErrTR.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CommErrTR.Size = new System.Drawing.Size(116, 17);
            this.CommErrTR.TabIndex = 48;
            this.CommErrTR.Text = "אין תקשורת ל TR";
            this.CommErrTR.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(724, 762);
            this.ControlBox = false;
            this.Controls.Add(this.CommErrTR);
            this.Controls.Add(this.FuelingNow);
            this.Controls.Add(this.OpenTrans);
            this.Controls.Add(this.DropWarn);
            this.Controls.Add(this.OverDropMoney);
            this.Controls.Add(this.NeedDrop);
            this.Controls.Add(this.FuelLimit);
            this.Controls.Add(this.VIUAttn);
            this.Controls.Add(this.MultiPump);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OtherMOP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.FileVersion);
            this.Controls.Add(this.CurrentImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Validation);
            this.Controls.Add(this.Pump);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RightToLeftLayout = true;
            this.Text = "דלק - הדגמת טרמינל";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.Pump.ResumeLayout(false);
            this.Pump.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Noz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox Pump;
        private System.Windows.Forms.PictureBox Noz;
        private System.Windows.Forms.Label Volume;
        private System.Windows.Forms.Label Money;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label PPV;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button CardTag;
        private System.Windows.Forms.RadioButton Soler;
        private System.Windows.Forms.RadioButton F95;
        public System.Windows.Forms.RadioButton Self;
        public System.Windows.Forms.RadioButton Full;
        public System.Windows.Forms.RadioButton NoService;
        public System.Windows.Forms.ListBox CardType;
        public System.Windows.Forms.CheckBox Validation;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox CurrentImage;
        private System.Windows.Forms.Label FileVersion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OtherMOP;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckBox MultiPump;
        public System.Windows.Forms.CheckBox VIUAttn;
        public System.Windows.Forms.CheckBox FuelLimit;
        private System.Windows.Forms.CheckBox NeedDrop;
        private System.Windows.Forms.CheckBox OverDropMoney;
        private System.Windows.Forms.CheckBox DropWarn;
        public System.Windows.Forms.CheckBox OpenTrans;
        public System.Windows.Forms.CheckBox FuelingNow;
        public System.Windows.Forms.CheckBox CommErrTR;
    }
}

