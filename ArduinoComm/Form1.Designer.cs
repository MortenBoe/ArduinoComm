namespace ArduinoComm
{
    partial class frmArduino
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
            this.btnOn = new System.Windows.Forms.Button();
            this.btnOff = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.ArduinoPort = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblPort = new System.Windows.Forms.Label();
            this.btnLesData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(26, 77);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(113, 23);
            this.btnOn.TabIndex = 0;
            this.btnOn.Text = "Test  \"A\"";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(26, 106);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(113, 23);
            this.btnOff.TabIndex = 1;
            this.btnOff.Text = "Test \"a\"";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(145, 204);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 2;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(29, 47);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(72, 24);
            this.cmbPort.TabIndex = 3;
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.cmbPort_SelectedIndexChanged);
            // 
            // ArduinoPort
            // 
            this.ArduinoPort.PortName = "COM_<Arduino>";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(29, 25);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(31, 16);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port";
            // 
            // btnLesData
            // 
            this.btnLesData.Location = new System.Drawing.Point(64, 203);
            this.btnLesData.Name = "btnLesData";
            this.btnLesData.Size = new System.Drawing.Size(75, 23);
            this.btnLesData.TabIndex = 5;
            this.btnLesData.Text = "LesData";
            this.btnLesData.UseVisualStyleBackColor = true;
            this.btnLesData.Click += new System.EventHandler(this.btnLesData_Click);
            // 
            // frmArduino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLesData);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.btnOn);
            this.Name = "frmArduino";
            this.Text = "Arduino";
            this.Load += new System.EventHandler(this.frmArduino_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.IO.Ports.SerialPort ArduinoPort;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnLesData;
    }
}

