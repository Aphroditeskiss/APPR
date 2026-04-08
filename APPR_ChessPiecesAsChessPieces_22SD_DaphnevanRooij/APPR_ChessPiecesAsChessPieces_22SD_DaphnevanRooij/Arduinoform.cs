using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace APPR_ChessPiecesAsChessPieces_22SD_DaphnevanRooij
{
    public partial class Arduinoform : Form
    {
        private Gameform mainform = null;

        public Arduinoform(Gameform c_mainForm)
        {
            InitializeComponent();
            mainform = c_mainForm;
        }

        private void Arduinoform_Load(object sender, EventArgs e)
        {
            btnSendMessage.Enabled = false;
            txbSendMessage.Enabled = false;
            btnZeroAll.Enabled = false;
        }

        // ====================== PORT SCAN ======================
        private void btnScanPortsDkal_Click(object sender, EventArgs e)
        {
            cbbSerialPortsDkal.Items.Clear();
            foreach (string port in SerialPort.GetPortNames())
            {
                cbbSerialPortsDkal.Items.Add(port);
            }
            if (cbbSerialPortsDkal.Items.Count > 0)
                cbbSerialPortsDkal.SelectedIndex = 0;
        }

        private void cbbSerialPortsDkal_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSerialPortOpenDkal.Enabled = true;
        }

        // ====================== OPEN PORT ======================
        private void btnSerialPortOpenDkal_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduinoConnection.IsOpen)
                {
                    serialPortArduinoConnection.Close();
                    btnSerialPortOpenDkal.Text = "Open port";
                    PrintLn("Port closed.", "R");
                }
                else
                {
                    serialPortArduinoConnection.PortName = cbbSerialPortsDkal.Text;
                    serialPortArduinoConnection.BaudRate = 115200;
                    serialPortArduinoConnection.Open();

                    btnSerialPortOpenDkal.Text = "Close port";
                    btnSendMessage.Enabled = true;
                    txbSendMessage.Enabled = true;
                    btnZeroAll.Enabled = true;

                    mainform.ArduinoConnected();
                    PrintLn("✅ Arduino connected successfully!", "G");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ====================== DATA RECEIVED ======================
        private void serialPortArduinoConnection_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                string data = serialPortArduinoConnection.ReadLine().Trim();
                PrintLn("Arduino: " + data, "G");
                // You can add more logic here later
            }));
        }

        private void rtbLogging_TextChanged(object sender, EventArgs e)
        {
            // Optional: Auto-scroll or react to Arduino messages
        }

        // ====================== SEND COMMAND ======================
        public void SendCommand(string command)
        {
            if (serialPortArduinoConnection.IsOpen)
            {
                try
                {
                    serialPortArduinoConnection.WriteLine(command);
                    PrintLn("Sent: " + command, "Y");
                }
                catch (Exception ex)
                {
                    PrintLn("Error sending: " + ex.Message, "R");
                }
            }
            else
            {
                PrintLn("Arduino is NOT connected! Please open the port first.", "R");
                // Only show MessageBox once, not every time
                // MessageBox.Show("Arduino is not connected!");   // ← commented out to stop spam
            }
        }

        public void WriteArduino(string command)
        {
            SendCommand(command);
        }

        private void PrintLn(string text, string color = "B")
        {
            switch (color.ToUpper())
            {
                case "R": rtbLogging.SelectionColor = System.Drawing.Color.Red; break;
                case "G": rtbLogging.SelectionColor = System.Drawing.Color.Green; break;
                case "Y": rtbLogging.SelectionColor = System.Drawing.Color.Orange; break;
                default: rtbLogging.SelectionColor = System.Drawing.Color.Black; break;
            }
            rtbLogging.AppendText(text + "\n");
            rtbLogging.ScrollToCaret();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbSendMessage.Text))
            {
                SendCommand(txbSendMessage.Text.Trim());
                txbSendMessage.Clear();
            }
        }

        private void btnZeroAll_Click(object sender, EventArgs e)
        {
            SendCommand("ZS:0");
        }

        private void btnPossibleMessages_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Common Commands:\n\nHS:xxx VS:xxx RS:xxx\nCS:1 / CS:0\nSS:1 / SS:0\nZS:0", "Help");
        }

        private void Arduinoform_Load_1(object sender, EventArgs e)
        {

        }

    }
}