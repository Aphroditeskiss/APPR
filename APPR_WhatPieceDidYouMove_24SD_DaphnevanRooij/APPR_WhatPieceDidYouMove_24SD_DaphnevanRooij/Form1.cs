using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPR_WhatPieceDidYouMove_24SD_DaphnevanRooij
{
    public partial class Form1 : Form
    {
        Piece currentPiece = null;
        PictureBox pcbFrom = null;
        PictureBox pcbTo = null;
        int horizontal = 0;
        int vertical = 0;
        string pieceOptions = "";
        List<Board> boardlist = new List<Board>();

        List<Piece> piecelist = new List<Piece>();

        public Form1()
        {
            InitializeComponent();
        }

        private void pcbAllPictureboxes_MouseDown(object sender, MouseEventArgs e)
        {
            ClearBoardcolors();
            pcbFrom = (PictureBox)sender;

            if (pcbFrom.Image != null && pcbFrom.BackColor == Color.Transparent)
            {
                LocationOfPicturebox(pcbFrom.Name);
                currentPiece = piecelist.FirstOrDefault(x => x.GetCurrentHorizontal() == horizontal && x.GetCurrentVertical() == vertical);

                GetBoardOptions();
                UpdateBoardpieceLocations();
                pcbFrom.DoDragDrop(pcbFrom.Image, DragDropEffects.Copy);
            }
        }

        private void pcbAllPictureboxes_DragDrop(object sender, DragEventArgs e)
        {
            pcbTo = (PictureBox)sender;
            Image getPicture = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            pcbTo.Image = getPicture;
            horizontal = Convert.ToInt32(pcbTo.Tag.ToString().Substring(0, 1));
            vertical = Convert.ToInt32(pcbTo.Tag.ToString().Substring(1, 1));
            currentPiece.SetLocation(horizontal, vertical);
            pcbFrom.Image = null;
            ClearBoardcolors();
        }

        private void pcbAllPictureboxes_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Bitmap) && ((PictureBox)sender).BackColor == Color.Green)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void GetBoardOptions()
        {
            pieceOptions = "";
            foreach (Board board in boardlist)
            {
                if (currentPiece != null)
                {
                    pieceOptions += currentPiece.GetMoveoptions(board.GetHorizontal(), board.GetVertical());

                }
            }
        }

        public void UpdateBoardpieceLocations()
        {
            for (int i = 0; i < pieceOptions.Length; i += 2)
            {
                foreach (PictureBox picturebox in gbxGame.Controls.OfType<PictureBox>())
                {
                    if (picturebox.Tag.ToString() == pieceOptions[i].ToString() + pieceOptions[i +1].ToString() && picturebox.Image == null)
                    {
                        picturebox.BackColor = Color.Green;
                    }
                }
            }
        }

        public void ClearAllImagesFromPlayingfield()
        {
            foreach (PictureBox picturebox in gbxGame.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = Color.LightGray;
                picturebox.Image = null;
            }
        }

        public void ClearBoardcolors()
        {
            foreach (PictureBox picturebox in gbxGame.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = Color.Transparent;
            }
        }

        private void LocationOfPicturebox(string pictureboxName)
        {
            switch(pictureboxName)
            {
                case "pcbOne": horizontal = 1; vertical = 1; break;
                case "pcbTwo": horizontal = 2; vertical = 1; break;
                case "pcbThree": horizontal = 3; vertical = 1; break;
                case "pcbFour": horizontal = 1; vertical = 2; break;
                case "pcbFive": horizontal = 2; vertical = 2; break;
                case "pcbSix":horizontal = 3; vertical = 2; break;
                case "pbcSeven":horizontal = 1; vertical = 3; break;
                case "pcbEight":horizontal = 2; vertical = 3; break;
                case "pcbNine": horizontal = 3; vertical = 3; break;
                default:
                    MessageBox.Show("Something went wrong with the location of your piece");
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (PictureBox picturebox in gbxGame.Controls.OfType<PictureBox>())
            {
                picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
                picturebox.AllowDrop = true;
            }

            boardlist.Clear();
            boardlist.Add(new Board(1, 1, "pcbOne"));
            boardlist.Add(new Board(2, 1, "pcbTwo"));
            boardlist.Add(new Board(3, 1, "pcbThree"));
            boardlist.Add(new Board(1, 2, "pcbFour"));
            boardlist.Add(new Board(2, 2, "pcbFive"));
            boardlist.Add(new Board(3, 2, "pcbSix"));
            boardlist.Add(new Board(1, 3, "pcbSeven"));
            boardlist.Add(new Board(2, 3, "pcbEight"));
            boardlist.Add(new Board(3, 3, "pcbNine"));

            piecelist.Clear();
            piecelist.Add(new Piece("Leon"));
            piecelist.Add(new Piece("Swain"));

            piecelist[0].SetLocation(1, 1);
            piecelist[1].SetLocation(2, 1);
        }
    }
}
