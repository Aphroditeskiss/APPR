namespace APPR_ArduinoConnect_Worksheet_Template2
{
    partial class Gameform
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
            this.btnSaveLocation = new System.Windows.Forms.Button();
            this.btnRunApplication = new System.Windows.Forms.Button();
            this.txbHorizontal = new System.Windows.Forms.TextBox();
            this.txbVertical = new System.Windows.Forms.TextBox();
            this.txbRotation = new System.Windows.Forms.TextBox();
            this.lblHorizontalText = new System.Windows.Forms.Label();
            this.lblVerticalText = new System.Windows.Forms.Label();
            this.lblRotationText = new System.Windows.Forms.Label();
            this.gbxLocations = new System.Windows.Forms.GroupBox();
            this.lblCurrentLocationtype = new System.Windows.Forms.Label();
            this.lblLocationtypeText = new System.Windows.Forms.Label();
            this.gbxConnectArduino = new System.Windows.Forms.GroupBox();
            this.lblConnected = new System.Windows.Forms.Label();
            this.lblConnectedText = new System.Windows.Forms.Label();
            this.btnConnectArduino = new System.Windows.Forms.Button();
            this.gbxLocationList = new System.Windows.Forms.GroupBox();
            this.lblArduinoBuisy = new System.Windows.Forms.Label();
            this.lblArduinoBuisyText = new System.Windows.Forms.Label();
            this.lbxLocationList = new System.Windows.Forms.ListBox();
            this.tmrArduino = new System.Windows.Forms.Timer(this.components);
            this.gbxLocations.SuspendLayout();
            this.gbxConnectArduino.SuspendLayout();
            this.gbxLocationList.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveLocation
            // 
            this.btnSaveLocation.Location = new System.Drawing.Point(8, 23);
            this.btnSaveLocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveLocation.Name = "btnSaveLocation";
            this.btnSaveLocation.Size = new System.Drawing.Size(155, 28);
            this.btnSaveLocation.TabIndex = 0;
            this.btnSaveLocation.Text = "Save location";
            this.btnSaveLocation.UseVisualStyleBackColor = true;
            this.btnSaveLocation.Click += new System.EventHandler(this.btnSaveLocation_Click);
            // 
            // btnRunApplication
            // 
            this.btnRunApplication.Location = new System.Drawing.Point(8, 59);
            this.btnRunApplication.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunApplication.Name = "btnRunApplication";
            this.btnRunApplication.Size = new System.Drawing.Size(155, 28);
            this.btnRunApplication.TabIndex = 3;
            this.btnRunApplication.Text = "Run application";
            this.btnRunApplication.UseVisualStyleBackColor = true;
            this.btnRunApplication.Click += new System.EventHandler(this.btnRunApplication_Click);
            // 
            // txbHorizontal
            // 
            this.txbHorizontal.Location = new System.Drawing.Point(171, 27);
            this.txbHorizontal.Margin = new System.Windows.Forms.Padding(4);
            this.txbHorizontal.Name = "txbHorizontal";
            this.txbHorizontal.Size = new System.Drawing.Size(60, 22);
            this.txbHorizontal.TabIndex = 4;
            this.txbHorizontal.Text = "0";
            // 
            // txbVertical
            // 
            this.txbVertical.Location = new System.Drawing.Point(264, 26);
            this.txbVertical.Margin = new System.Windows.Forms.Padding(4);
            this.txbVertical.Name = "txbVertical";
            this.txbVertical.Size = new System.Drawing.Size(60, 22);
            this.txbVertical.TabIndex = 5;
            this.txbVertical.Text = "0";
            // 
            // txbRotation
            // 
            this.txbRotation.Location = new System.Drawing.Point(360, 27);
            this.txbRotation.Margin = new System.Windows.Forms.Padding(4);
            this.txbRotation.Name = "txbRotation";
            this.txbRotation.Size = new System.Drawing.Size(60, 22);
            this.txbRotation.TabIndex = 6;
            this.txbRotation.Text = "0";
            // 
            // lblHorizontalText
            // 
            this.lblHorizontalText.AutoSize = true;
            this.lblHorizontalText.Location = new System.Drawing.Point(167, 7);
            this.lblHorizontalText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHorizontalText.Name = "lblHorizontalText";
            this.lblHorizontalText.Size = new System.Drawing.Size(67, 16);
            this.lblHorizontalText.TabIndex = 13;
            this.lblHorizontalText.Text = "Horizontal";
            // 
            // lblVerticalText
            // 
            this.lblVerticalText.AutoSize = true;
            this.lblVerticalText.Location = new System.Drawing.Point(260, 7);
            this.lblVerticalText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerticalText.Name = "lblVerticalText";
            this.lblVerticalText.Size = new System.Drawing.Size(52, 16);
            this.lblVerticalText.TabIndex = 14;
            this.lblVerticalText.Text = "Vertical";
            // 
            // lblRotationText
            // 
            this.lblRotationText.AutoSize = true;
            this.lblRotationText.Location = new System.Drawing.Point(356, 7);
            this.lblRotationText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRotationText.Name = "lblRotationText";
            this.lblRotationText.Size = new System.Drawing.Size(57, 16);
            this.lblRotationText.TabIndex = 15;
            this.lblRotationText.Text = "Rotation";
            // 
            // gbxLocations
            // 
            this.gbxLocations.Controls.Add(this.lblCurrentLocationtype);
            this.gbxLocations.Controls.Add(this.lblLocationtypeText);
            this.gbxLocations.Controls.Add(this.btnSaveLocation);
            this.gbxLocations.Controls.Add(this.lblRotationText);
            this.gbxLocations.Controls.Add(this.btnRunApplication);
            this.gbxLocations.Controls.Add(this.lblVerticalText);
            this.gbxLocations.Controls.Add(this.txbHorizontal);
            this.gbxLocations.Controls.Add(this.lblHorizontalText);
            this.gbxLocations.Controls.Add(this.txbVertical);
            this.gbxLocations.Controls.Add(this.txbRotation);
            this.gbxLocations.Location = new System.Drawing.Point(16, 86);
            this.gbxLocations.Margin = new System.Windows.Forms.Padding(4);
            this.gbxLocations.Name = "gbxLocations";
            this.gbxLocations.Padding = new System.Windows.Forms.Padding(4);
            this.gbxLocations.Size = new System.Drawing.Size(433, 102);
            this.gbxLocations.TabIndex = 16;
            this.gbxLocations.TabStop = false;
            this.gbxLocations.Text = "Save location";
            // 
            // lblCurrentLocationtype
            // 
            this.lblCurrentLocationtype.AutoSize = true;
            this.lblCurrentLocationtype.Location = new System.Drawing.Point(317, 71);
            this.lblCurrentLocationtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentLocationtype.Name = "lblCurrentLocationtype";
            this.lblCurrentLocationtype.Size = new System.Drawing.Size(16, 16);
            this.lblCurrentLocationtype.TabIndex = 20;
            this.lblCurrentLocationtype.Text = "...";
            // 
            // lblLocationtypeText
            // 
            this.lblLocationtypeText.AutoSize = true;
            this.lblLocationtypeText.Location = new System.Drawing.Point(171, 71);
            this.lblLocationtypeText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocationtypeText.Name = "lblLocationtypeText";
            this.lblLocationtypeText.Size = new System.Drawing.Size(128, 16);
            this.lblLocationtypeText.TabIndex = 19;
            this.lblLocationtypeText.Text = "Current locationtype:";
            // 
            // gbxConnectArduino
            // 
            this.gbxConnectArduino.Controls.Add(this.lblConnected);
            this.gbxConnectArduino.Controls.Add(this.lblConnectedText);
            this.gbxConnectArduino.Controls.Add(this.btnConnectArduino);
            this.gbxConnectArduino.Location = new System.Drawing.Point(16, 15);
            this.gbxConnectArduino.Margin = new System.Windows.Forms.Padding(4);
            this.gbxConnectArduino.Name = "gbxConnectArduino";
            this.gbxConnectArduino.Padding = new System.Windows.Forms.Padding(4);
            this.gbxConnectArduino.Size = new System.Drawing.Size(433, 64);
            this.gbxConnectArduino.TabIndex = 17;
            this.gbxConnectArduino.TabStop = false;
            this.gbxConnectArduino.Text = "Connect arduino";
            // 
            // lblConnected
            // 
            this.lblConnected.AutoSize = true;
            this.lblConnected.Location = new System.Drawing.Point(327, 30);
            this.lblConnected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(25, 16);
            this.lblConnected.TabIndex = 18;
            this.lblConnected.Text = "No";
            // 
            // lblConnectedText
            // 
            this.lblConnectedText.AutoSize = true;
            this.lblConnectedText.Location = new System.Drawing.Point(185, 30);
            this.lblConnectedText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectedText.Name = "lblConnectedText";
            this.lblConnectedText.Size = new System.Drawing.Size(122, 16);
            this.lblConnectedText.TabIndex = 17;
            this.lblConnectedText.Text = "Arduino connected:";
            // 
            // btnConnectArduino
            // 
            this.btnConnectArduino.Location = new System.Drawing.Point(8, 23);
            this.btnConnectArduino.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnectArduino.Name = "btnConnectArduino";
            this.btnConnectArduino.Size = new System.Drawing.Size(155, 28);
            this.btnConnectArduino.TabIndex = 16;
            this.btnConnectArduino.Text = "Connect arduino";
            this.btnConnectArduino.UseVisualStyleBackColor = true;
            this.btnConnectArduino.Click += new System.EventHandler(this.btnConnectArduino_Click);
            // 
            // gbxLocationList
            // 
            this.gbxLocationList.Controls.Add(this.lblArduinoBuisy);
            this.gbxLocationList.Controls.Add(this.lblArduinoBuisyText);
            this.gbxLocationList.Controls.Add(this.lbxLocationList);
            this.gbxLocationList.Location = new System.Drawing.Point(457, 15);
            this.gbxLocationList.Margin = new System.Windows.Forms.Padding(4);
            this.gbxLocationList.Name = "gbxLocationList";
            this.gbxLocationList.Padding = new System.Windows.Forms.Padding(4);
            this.gbxLocationList.Size = new System.Drawing.Size(323, 174);
            this.gbxLocationList.TabIndex = 18;
            this.gbxLocationList.TabStop = false;
            this.gbxLocationList.Text = "Location list";
            // 
            // lblArduinoBuisy
            // 
            this.lblArduinoBuisy.AutoSize = true;
            this.lblArduinoBuisy.Location = new System.Drawing.Point(113, 30);
            this.lblArduinoBuisy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArduinoBuisy.Name = "lblArduinoBuisy";
            this.lblArduinoBuisy.Size = new System.Drawing.Size(25, 16);
            this.lblArduinoBuisy.TabIndex = 2;
            this.lblArduinoBuisy.Text = "No";
            // 
            // lblArduinoBuisyText
            // 
            this.lblArduinoBuisyText.AutoSize = true;
            this.lblArduinoBuisyText.Location = new System.Drawing.Point(8, 30);
            this.lblArduinoBuisyText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArduinoBuisyText.Name = "lblArduinoBuisyText";
            this.lblArduinoBuisyText.Size = new System.Drawing.Size(91, 16);
            this.lblArduinoBuisyText.TabIndex = 1;
            this.lblArduinoBuisyText.Text = "Arduino buisy:";
            // 
            // lbxLocationList
            // 
            this.lbxLocationList.FormattingEnabled = true;
            this.lbxLocationList.ItemHeight = 16;
            this.lbxLocationList.Location = new System.Drawing.Point(8, 55);
            this.lbxLocationList.Margin = new System.Windows.Forms.Padding(4);
            this.lbxLocationList.Name = "lbxLocationList";
            this.lbxLocationList.Size = new System.Drawing.Size(305, 116);
            this.lbxLocationList.TabIndex = 0;
            // 
            // Gameform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 193);
            this.Controls.Add(this.gbxLocationList);
            this.Controls.Add(this.gbxConnectArduino);
            this.Controls.Add(this.gbxLocations);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Gameform";
            this.Text = "Gameform";
            this.Load += new System.EventHandler(this.Gameform_Load);
            this.gbxLocations.ResumeLayout(false);
            this.gbxLocations.PerformLayout();
            this.gbxConnectArduino.ResumeLayout(false);
            this.gbxConnectArduino.PerformLayout();
            this.gbxLocationList.ResumeLayout(false);
            this.gbxLocationList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveLocation;
        private System.Windows.Forms.Button btnRunApplication;
        private System.Windows.Forms.TextBox txbHorizontal;
        private System.Windows.Forms.TextBox txbVertical;
        private System.Windows.Forms.TextBox txbRotation;
        private System.Windows.Forms.Label lblHorizontalText;
        private System.Windows.Forms.Label lblVerticalText;
        private System.Windows.Forms.Label lblRotationText;
        private System.Windows.Forms.GroupBox gbxLocations;
        private System.Windows.Forms.GroupBox gbxConnectArduino;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Label lblConnectedText;
        private System.Windows.Forms.Button btnConnectArduino;
        private System.Windows.Forms.GroupBox gbxLocationList;
        private System.Windows.Forms.ListBox lbxLocationList;
        private System.Windows.Forms.Timer tmrArduino;
        private System.Windows.Forms.Label lblArduinoBuisy;
        private System.Windows.Forms.Label lblArduinoBuisyText;
        private System.Windows.Forms.Label lblLocationtypeText;
        private System.Windows.Forms.Label lblCurrentLocationtype;
    }
}