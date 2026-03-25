using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPR_Gamelogic_24SD_DaphnevanRooij
{
    public partial class Form1 : Form
    {

        PictureBox pcbFrom = null;
        PictureBox pcbTo = null;

        Color notSelectableColor = Color.Gray;
        Color selectableColor = Color.Transparent;
        Color playerOneColor = Color.Red;
        Color playerTwoColor = Color.Blue;
        Color winnerColor = Color.Orange;
        Color allowDropColor = Color.Green;

        Color playerTurnColor;

        string playerturn = "";
        GroupBox selectedGroupbox = null;
        Random randomgenerator = new Random();
        int championsPickedCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void pcbAllChampions_MouseDown(object sender, MouseEventArgs e)
        {
            pcbFrom = (PictureBox)sender;
            if (pcbFrom.BackColor == selectableColor)
            {
                foreach (PictureBox picturebox in selectedGroupbox.Controls.OfType<PictureBox>())
                {
                    if (picturebox.Image == null)
                    {
                        picturebox.BackColor = allowDropColor;
                    }
                    else
                    {
                        picturebox.BackColor = playerTurnColor;
                    }
                }
                pcbFrom.DoDragDrop(pcbFrom.Image, DragDropEffects.Copy);
            }
        }

        private void pcbAllPlayers_DragDrop(object sender, DragEventArgs e)
        {
            pcbFrom.BackColor = notSelectableColor;
            pcbTo = (PictureBox)sender;
            Image getPicture = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            pcbTo.Image = getPicture;

            ResetPlayerColors();

            ChangeTurns();
            championsPickedCount++;
            if (championsPickedCount == 6)
            {
                DisableAllChampions();
                lblGamelabel.Text = "All champions have been picked. Time to fight!";
                btnFight.Enabled = true;
                btnStart.Enabled = false;
            }
        }

        private void pcbAllPlayers_DragOver(object sender, DragEventArgs e)
        {
            pcbTo = (PictureBox)sender;
            if (pcbTo.BackColor == allowDropColor)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnFight.Enabled = false;

            foreach (PictureBox picturebox in gbxChampions.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = notSelectableColor;
                picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            foreach (PictureBox picturebox in gbxPlayerOne.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = playerOneColor;
                picturebox.AllowDrop = true;
                picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            foreach (PictureBox picturebox in gbxPlayerTwo.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = playerTwoColor;
                picturebox.AllowDrop = true;
                picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        public void EnableNotPickedChampions()
        {
            foreach (PictureBox picturebox in gbxChampions.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = selectableColor;
            }
        }

        public void DisableAllChampions()
        {
            foreach (PictureBox picturebox in gbxChampions.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = notSelectableColor;
            }
        }

        public void ChangeTurns()
        {
            if (playerturn == "playerOne")
            {
                playerturn = "playerTwo";
                lblGamelabel.Text = "Player two must pick a champion!";
                playerTurnColor = playerTwoColor;
                selectedGroupbox = gbxPlayerTwo;
            }
            else
            {
                playerturn = "playerOne";
                lblGamelabel.Text = "Player one must pick a champion.";
                playerTurnColor = playerOneColor;
                selectedGroupbox = gbxPlayerOne;
            }

            ResetPlayerColors();
        }

        public void ResetPlayerColors()
        {
            foreach (PictureBox picturebox in selectedGroupbox.Controls.OfType<PictureBox>())
            {
                if (picturebox.Image == null)
                {
                    picturebox.BackColor = playerTurnColor;
                }
                else
                {
                    picturebox.BackColor = Color.Transparent;
                }
            }
        }

        private void btnFight_Click(object sender, EventArgs e)
        {
            int playerOnePowerlevel = randomgenerator.Next(1, 1001);
            lblPlayerOnePowerlevel.Text = "Powerlevel: " + playerOnePowerlevel.ToString();
            int playerTwoPowerlevel = randomgenerator.Next(1, 1001);
            lblPlayerTwoPowerlevel.Text = "Powerlevel: " + playerTwoPowerlevel.ToString();

            if (playerOnePowerlevel > playerTwoPowerlevel)
            {
                gbxPlayerOne.BackColor = winnerColor;
                lblGamelabel.Text = "Player one won with a powerlevel of: " + playerOnePowerlevel;
            }
            else if (playerOnePowerlevel < playerTwoPowerlevel)
            {
                gbxPlayerTwo.BackColor = winnerColor;
                lblGamelabel.Text = "Player two won with a powerlevel of: " + playerTwoPowerlevel;
            }
            else
            {
                gbxPlayerOne.BackColor = winnerColor;
                gbxPlayerTwo.BackColor = winnerColor;
                lblGamelabel.Text = "It's a draw, they both have a powerlevel of: " + playerOnePowerlevel;
            }
            btnStart.Enabled = true;
            btnFight.Enabled = false;
        }
       
        public void RestartGame()
        {
            btnStart.Enabled = true;
            EnableNotPickedChampions();
            playerturn = "playerTwo";
            championsPickedCount = 0;
            lblGamelabel.Text = "Player one must pick a champion.";
            ChangeTurns();
            gbxPlayerOne.BackColor = Color.Transparent;
            gbxPlayerTwo.BackColor = Color.Transparent;
            lblPlayerOnePowerlevel.Text = "Powerlevel: 0";
            lblPlayerTwoPowerlevel.Text = "Powerlevel: 0";
            foreach (PictureBox picturebox in gbxPlayerOne.Controls.OfType<PictureBox>())
            {
                picturebox.Image = null;
                picturebox.BackColor = playerOneColor;
            }
            foreach (PictureBox picturebox in gbxPlayerTwo.Controls.OfType<PictureBox>())
            {
                picturebox.Image = null;
                picturebox.BackColor = playerTwoColor;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
