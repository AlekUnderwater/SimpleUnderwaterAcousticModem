namespace SimpleUnderwaterAcousticModem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.startStopBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.terminalTxb = new System.Windows.Forms.RichTextBox();
            this.textToSendTxb = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.defIntDurationCbx = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.b1DurationCbx = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.windowSizeCbx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sampleRateHzCbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.terminalGroup = new System.Windows.Forms.GroupBox();
            this.thresholdGroup = new System.Windows.Forms.GroupBox();
            this.thresholdTkb = new System.Windows.Forms.TrackBar();
            this.mainToolStrip.SuspendLayout();
            this.settingsGroup.SuspendLayout();
            this.terminalGroup.SuspendLayout();
            this.thresholdGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTkb)).BeginInit();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStopBtn,
            this.toolStripSeparator1,
            this.toolStripLabel1});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(857, 30);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // startStopBtn
            // 
            this.startStopBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startStopBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startStopBtn.Image = ((System.Drawing.Image)(resources.GetObject("startStopBtn.Image")));
            this.startStopBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startStopBtn.Name = "startStopBtn";
            this.startStopBtn.Size = new System.Drawing.Size(66, 27);
            this.startStopBtn.Text = "START";
            this.startStopBtn.Click += new System.EventHandler(this.startStopBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.IsLink = true;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(158, 27);
            this.toolStripLabel1.Text = "www.unavlab.com";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // terminalTxb
            // 
            this.terminalTxb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.terminalTxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.terminalTxb.Location = new System.Drawing.Point(6, 29);
            this.terminalTxb.Name = "terminalTxb";
            this.terminalTxb.ReadOnly = true;
            this.terminalTxb.Size = new System.Drawing.Size(492, 440);
            this.terminalTxb.TabIndex = 2;
            this.terminalTxb.Text = "";
            this.terminalTxb.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // textToSendTxb
            // 
            this.textToSendTxb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textToSendTxb.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textToSendTxb.Location = new System.Drawing.Point(6, 475);
            this.textToSendTxb.MaxLength = 512;
            this.textToSendTxb.Name = "textToSendTxb";
            this.textToSendTxb.Size = new System.Drawing.Size(384, 30);
            this.textToSendTxb.TabIndex = 3;
            this.textToSendTxb.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendBtn.Enabled = false;
            this.sendBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendBtn.Location = new System.Drawing.Point(396, 475);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(102, 30);
            this.sendBtn.TabIndex = 4;
            this.sendBtn.Text = "SEND";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // settingsGroup
            // 
            this.settingsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsGroup.Controls.Add(this.defIntDurationCbx);
            this.settingsGroup.Controls.Add(this.label4);
            this.settingsGroup.Controls.Add(this.b1DurationCbx);
            this.settingsGroup.Controls.Add(this.label3);
            this.settingsGroup.Controls.Add(this.windowSizeCbx);
            this.settingsGroup.Controls.Add(this.label2);
            this.settingsGroup.Controls.Add(this.sampleRateHzCbx);
            this.settingsGroup.Controls.Add(this.label1);
            this.settingsGroup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsGroup.Location = new System.Drawing.Point(522, 34);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(323, 410);
            this.settingsGroup.TabIndex = 5;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "Settings";
            // 
            // defIntDurationCbx
            // 
            this.defIntDurationCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defIntDurationCbx.FormattingEnabled = true;
            this.defIntDurationCbx.Location = new System.Drawing.Point(6, 318);
            this.defIntDurationCbx.Name = "defIntDurationCbx";
            this.defIntDurationCbx.Size = new System.Drawing.Size(203, 31);
            this.defIntDurationCbx.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Defence interval duration";
            // 
            // b1DurationCbx
            // 
            this.b1DurationCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.b1DurationCbx.FormattingEnabled = true;
            this.b1DurationCbx.Location = new System.Drawing.Point(6, 231);
            this.b1DurationCbx.Name = "b1DurationCbx";
            this.b1DurationCbx.Size = new System.Drawing.Size(203, 31);
            this.b1DurationCbx.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Bit \"1\" duration";
            // 
            // windowSizeCbx
            // 
            this.windowSizeCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.windowSizeCbx.FormattingEnabled = true;
            this.windowSizeCbx.Location = new System.Drawing.Point(6, 145);
            this.windowSizeCbx.Name = "windowSizeCbx";
            this.windowSizeCbx.Size = new System.Drawing.Size(203, 31);
            this.windowSizeCbx.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Window size";
            // 
            // sampleRateHzCbx
            // 
            this.sampleRateHzCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sampleRateHzCbx.FormattingEnabled = true;
            this.sampleRateHzCbx.Location = new System.Drawing.Point(6, 66);
            this.sampleRateHzCbx.Name = "sampleRateHzCbx";
            this.sampleRateHzCbx.Size = new System.Drawing.Size(203, 31);
            this.sampleRateHzCbx.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Samplerate, Hz";
            // 
            // terminalGroup
            // 
            this.terminalGroup.Controls.Add(this.terminalTxb);
            this.terminalGroup.Controls.Add(this.textToSendTxb);
            this.terminalGroup.Controls.Add(this.sendBtn);
            this.terminalGroup.Enabled = false;
            this.terminalGroup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.terminalGroup.Location = new System.Drawing.Point(12, 34);
            this.terminalGroup.Name = "terminalGroup";
            this.terminalGroup.Size = new System.Drawing.Size(504, 511);
            this.terminalGroup.TabIndex = 6;
            this.terminalGroup.TabStop = false;
            this.terminalGroup.Text = "Terminal";
            // 
            // thresholdGroup
            // 
            this.thresholdGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdGroup.Controls.Add(this.thresholdTkb);
            this.thresholdGroup.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thresholdGroup.Location = new System.Drawing.Point(522, 450);
            this.thresholdGroup.Name = "thresholdGroup";
            this.thresholdGroup.Size = new System.Drawing.Size(323, 95);
            this.thresholdGroup.TabIndex = 6;
            this.thresholdGroup.TabStop = false;
            this.thresholdGroup.Text = "Threshold";
            // 
            // thresholdTkb
            // 
            this.thresholdTkb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdTkb.Location = new System.Drawing.Point(6, 33);
            this.thresholdTkb.Maximum = 5000;
            this.thresholdTkb.Minimum = 100;
            this.thresholdTkb.Name = "thresholdTkb";
            this.thresholdTkb.Size = new System.Drawing.Size(311, 56);
            this.thresholdTkb.SmallChange = 100;
            this.thresholdTkb.TabIndex = 0;
            this.thresholdTkb.TickFrequency = 100;
            this.thresholdTkb.Value = 500;
            this.thresholdTkb.Scroll += new System.EventHandler(this.thresholdTkb_Scroll);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 576);
            this.Controls.Add(this.thresholdGroup);
            this.Controls.Add(this.terminalGroup);
            this.Controls.Add(this.settingsGroup);
            this.Controls.Add(this.mainToolStrip);
            this.Name = "MainForm";
            this.Text = "The Simpliest Underwater Acoustic Modem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            this.terminalGroup.ResumeLayout(false);
            this.terminalGroup.PerformLayout();
            this.thresholdGroup.ResumeLayout(false);
            this.thresholdGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTkb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton startStopBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RichTextBox terminalTxb;
        private System.Windows.Forms.TextBox textToSendTxb;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.GroupBox terminalGroup;
        private System.Windows.Forms.ComboBox sampleRateHzCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox thresholdGroup;
        private System.Windows.Forms.ComboBox windowSizeCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox defIntDurationCbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox b1DurationCbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar thresholdTkb;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}

