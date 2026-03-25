using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPR_Winning_24SD_DaphnevanRooij
{
    public partial class Form1 : Form
    {
        Piece playerOne = null;
        Piece playerTwo = null;
        List<string> winList = null;
        List<Board> boardList = null;

        Color defaultColor = Color.LightSlateGray;
        Color colorPlayerOne = Color.Orange;
        Color colorPlayerTwo = Color.Aqua;
        PictureBox currentPpiecebox = null;
        Board currentBoard = null;

        int horizontal = 0;
        int vertical = 0;
        string currentTurn = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void pcbAllGame_Click(object sender, EventArgs e)
        {
            currentPicturebox = (PictureBox)sender;
            horizontal = Convert.ToInt32(currentPicturebox.Tag.ToString().Substring(0, 1));
            vertical = Convert.ToInt32(currentPicturebox.Tag.ToString().Substring(1, 1));
            currentBoard = boardList.FirstOrDefault(x => x.GetHorizontal() == horizontal && x.GetVertical() == vertical);

            if (currentTurn == "PlayerOne")
            {
                if (currentPicturebox.BackColor == defaultColor)
                {
                    currentPicturebox.BackColor = colorPlayerOne;
                    currentBoard.SetPiece(playerOne);

                    currentTurn = currentTurn == "PlayerOne" ? "PlayerTwo" : "PlayerOne";
                    pcbCurrentTurn.BackColor = pcbCurrentTurn.BackColor == colorPlayerOne ? colorPlayerTwo : colorPlayerOne;

                    CheckWinner();
                }
            }
            else
            {
                if (
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetGame();
            playerOne = new Piece(colorPlayerOne);
            playerTwo = new Piece(colorPlayerTwo);

            winList = new List<string>();
            winList.Add("012");
            winList.Add("345");
            winList.Add("678");
            winList.Add("036");
            winList.Add("147");
            winList.Add("258");
            winList.Add("246");
            winList.Add("048");
        }
    }
}
