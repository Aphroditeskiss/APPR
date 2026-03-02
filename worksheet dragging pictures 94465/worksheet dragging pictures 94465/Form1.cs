using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using worksheet_dragging_pictures_94465.Properties;

namespace worksheet_dragging_pictures_94465
{
    public partial class Form1 : Form
    {
        PictureBox pcbFrom = null;
        PictureBox pcbTo = null;

        Fighter fighterOne = null;
        Fighter fighterTwo = null;
        Fighter fighterThree = null;

        int correctAnswercount = 0;
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void pcbOptions_MouseDown(object sender, MouseEventArgs e)
        {
            pcbFrom = (PictureBox)sender;
            if (pcbFrom.BackColor == Color.Transparent)
            {
                foreach (PictureBox pcb in gbxPlayer.Controls.OfType<PictureBox>())
                {
                    if (pcb.Image == null)
                    {
                        pcb.BackColor = Color.Green;
                    }
                }
                pcbFrom.DoDragDrop(pcbFrom.Image, DragDropEffects.Copy);
            }
        }

        private void pcbAllPlayer_DragDrop(object sender, DragEventArgs e)
        {
            pcbTo = (PictureBox)sender;
            Image getImage = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            pcbTo.Image = getImage;
            if (pcbTo.Tag.ToString() == "1")
            {
                fighterOne = new Fighter(pcbFrom.Name.Remove(0, 3));
                fighterOne.SetOnBoard(true, 1);
            }
            else if (pcbTo.Tag.ToString() == "2")
            {
                fighterTwo = new Fighter(pcbFrom.Name.Remove(0, 3));
                fighterTwo.SetOnBoard(true, 2);
            }
            else if (pcbTo.Tag.ToString() == "3")
            {
                fighterThree = new Fighter(pcbFrom.Name.Remove(0, 3));
                fighterThree.SetOnBoard(true, 3);
            }
            ClearBoardcolors();
            if (fighterOne != null && fighterTwo != null && fighterThree != null)
            {
                btnCheck.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            fighterOne = null;
            fighterTwo = null;
            fighterThree = null;

            foreach (PictureBox pcb in gbxPlayer.Controls.OfType<PictureBox>())
            {
                pcb.BackColor = Color.Transparent;
            }

            foreach (PictureBox pcb in gbxOptions.Controls.OfType<PictureBox>())
            {
                pcb.BackColor = Color.Transparent;
            }
        }

        private void pcbAllPlayer_DragOver(object sender, DragEventArgs e)
        {
            pcbTo = (PictureBox)sender;
            if (pcbTo.BackColor == Color.Green)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckCorrect(pcbBotOne, fighterOne);
            CheckCorrect(pcbBotTwo, fighterTwo);
            CheckCorrect(pcbBotThree, fighterThree);

            if (correctAnswercount == 0)
            {
                MessageBox.Show("You got nothing right lol");
            }
            else if (correctAnswercount == 1 || correctAnswercount == 2)
            {
                MessageBox.Show("Noice, you got " + correctAnswercount + " right.");
            }
            else
            {
                MessageBox.Show("Whoa ur kinda goated");
            }
            ResetGame();
        }

        private void ResetGame()
        {
            btnCheck.Enabled = false;
            btnStart.Enabled = true;
            correctAnswercount = 0;

            foreach (PictureBox pcb in gbxPlayer.Controls.OfType<PictureBox>())
            {
                pcb.BackColor = Color.Gray;
                pcb.Image = null;
                pcb.AllowDrop = true;
            }

            foreach (PictureBox pcb in gbxBot.Controls.OfType<PictureBox>())
            {
                pcb.BackColor = Color.Gray;
                pcb.Image = null;
            }

            foreach (PictureBox pcb in gbxOptions.Controls.OfType<PictureBox>())
            {
                pcb.BackColor = Color.Gray;
            }
        }

        private void ClearBoardcolors()
        {
            foreach (PictureBox pcb in gbxPlayer.Controls.OfType<PictureBox>())
            {
                pcb.BackColor = Color.Transparent;
            }
        }

        private void CheckCorrect(PictureBox pcbToCheck, Fighter fighterToCheck)
        {
            int randomNumber = random.Next(1, 4);
            string randomName = "";
            if (randomNumber == 1)
            {
                randomName = "Rock";
            }
            else if (randomNumber == 2)
            {
                randomName = "Paper";
            }
            else if (randomNumber == 3)
            {
                randomName = "Scissor";
            }
            Bitmap bm = new Bitmap("C:\\Users\\otaku\\source\repos\\worksheet dragging pictures 94465\\worksheet dragging pictures 94465\\Resources\\" + randomName + ".png");
            pcbToCheck.Image = bm;

            if (randomName == fighterToCheck.GetName())
            {
                correctAnswercount++;
                pcbToCheck.BackColor = Color.LightGreen;
            }
            else
            {
                pcbToCheck.BackColor = Color.Red;
            }
        }
    }
}
