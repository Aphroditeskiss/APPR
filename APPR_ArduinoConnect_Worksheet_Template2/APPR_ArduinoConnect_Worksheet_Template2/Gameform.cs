using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPR_ArduinoConnect_Worksheet_Template2
{

    public partial class Gameform : Form
    {
        int horizontal, vertical, rotation; string locationtype = ""; Arduinoform arduinoform = null; Location newLocation = null; List<Location> locationlist = null;
        int moveArduinoCounter = 0;
        string commando = "";
        Location currentLocation = null; int currentlocationCount = 0; bool moveBuisy = false;
        public Gameform()
        {
            InitializeComponent();
        }
        private void Gameform_Load(object sender, EventArgs e)
        {
            arduinoform = new Arduinoform(this);
            locationlist = new List<Location>();
            btnRunApplication.Enabled = false;
        }
        private void btnConnectArduino_Click(object sender, EventArgs e)
        {
            arduinoform.Show();
            arduinoform.Left = this.Left + this.Width + 10;
            arduinoform.Top = this.Top;
        }
        public void ArduinoConnected()
        {
            btnRunApplication.Enabled = true;
            lblConnected.Text = "Yes";
            lblCurrentLocationtype.Text = "PickUp";
            locationtype = lblCurrentLocationtype.Text;
        }
        public void SwitchLocationtype()
        {
            if (locationtype == "PickUp")
            {
                locationtype = "DropOff";
            }
            else
            {
            }
            locationtype = "PickUp";
            lblCurrentLocationtype.Text = locationtype;
        }

        private void btnSaveLocation_Click(object sender, EventArgs e)
        {
            try
            {
                horizontal = Convert.ToInt32(txbHorizontal.Text);
                vertical = Convert.ToInt32(txbVertical.Text);
                rotation = Convert.ToInt32(txbRotation.Text);
                horizontal = CheckNumber(horizontal);
                vertical = CheckNumber(vertical);
                rotation = CheckNumber(rotation);

                if (horizontal != -1 && vertical != -1 && rotation != -1)
                {
                    newLocation = new Location(horizontal, vertical, rotation, locationtype);
                    locationlist.Add(newLocation);
                    lbxLocationList.Items.Add(newLocation.GetHorizontal() + " " + newLocation.GetVertical() + " " + newLocation.GetRotation() + " " + newLocation.GetLocationtype());
                    SwitchLocationtype();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Only numbers are allowed.");
            }
        }
        public int CheckNumber(int number)
        {
            if (number < 0 || number > 1500)
            {
                MessageBox.Show("Numbers must be between 0 and 1500"); number = -1;
            }
            return number;
        }
        private void tmrArduino_Tick(object sender, EventArgs e)
        {
            lblArduinoBuisy.Text = "Yes";
            if (moveArduinoCounter == 0)
            {
                commando = $"RS: {currentLocation.GetRotation()}";
            }
            else if (moveArduinoCounter == 1)
            {
                commando = $"HS: {currentLocation.GetHorizontal()}";
            }
            else if (moveArduinoCounter == 2)
            {
                commando = $"VS: {currentLocation.GetVertical()}";
            }
            if (currentLocation.GetLocationtype() == "PickUp")
            {
                if (moveArduinoCounter == 3)
                {
                    commando = "CS:1";
                }
                else if (moveArduinoCounter == 4)
                {
                    commando = "SS:1";
                }
            }
            else if (currentLocation.GetLocationtype() == "DropOff")
            {
                if (moveArduinoCounter == 5)
                {
                    commando = "CS:0";
                }
                else if (moveArduinoCounter == 6)
                {
                    commando = "SS:0";
                }
            }
            else if (moveArduinoCounter == 7)
            {
                commando = "ZS:1";

                if (moveBuisy == false)
                {
                    moveBuisy = true;
                    arduinoform.WriteArduino(commando);
                }
            }
            }

        private void btnConnectArduino_Click_1(object sender, EventArgs e)
        {

        }

        public void NextArduinoStep()
        {
            moveBuisy = false;
            moveArduinoCounter++;
            if (moveArduinoCounter == 8)
            {
                moveArduinoCounter = 0;
                currentlocationCount++;
                if (currentlocationCount <= locationlist.Count - 1)
                {
                    currentLocation = locationlist[currentlocationCount];
                    lbxLocationList.SelectedIndex = currentlocationCount;
                }
                else
                {
                    arduinoform.WriteArduino("ZS:0");
                    MessageBox.Show("The arduino is finished and will now reset.");
                    lbxLocationList.ClearSelected();
                    tmrArduino.Stop();
                }
            }
        }

        private void btnRunApplication_Click(object sender, EventArgs e)
        {

        }
    }
}
            