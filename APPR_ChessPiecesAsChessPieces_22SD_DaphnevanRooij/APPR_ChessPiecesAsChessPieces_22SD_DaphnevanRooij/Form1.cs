using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPR_ChessPiecesAsChessPieces_22SD_DaphnevanRooij
{
    public partial class Form1 : Form
    {
        Piece currentPiece = null;
        PictureBox pcbFrom = null;
        PictureBox pcbTo = null;
        int horizontal = 0;
        int vertical = 0;
        string pieceOptions = "";
        List<Board> boardList = new List<Board>();

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pcbAll_MouseDown(object sender, MouseEventArgs e)
        {
            ClearBoardcolors();
            pcbFrom = (PictureBox)sender;

            if (pcbFrom.Image != null)
            {
                GetBoardOptions();
                UpdateBoardpieceLocations();
                pcbFrom.DoDragDrop(pcbFrom.Image, DragDropEffects.Copy);
            }
        }

        private void pcbAll_DragDrop(object sender, DragEventArgs e)
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

        private void pcbAll_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap) && ((PictureBox)sender).BackColor == Color.Green)
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
            foreach (PictureBox picturebox in  gbxPlayingField.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = Color.LightGray;
                picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
                picturebox.AllowDrop = true;
            }

            boardList.Clear();
            boardList.Add(new Board(1, 1, "pcbOne"));
            boardList.Add(new Board(2, 1, "pcbTwo"));
            boardList.Add(new Board(3, 1, "pcbThree"));
            boardList.Add(new Board(1, 2, "pcbFour"));
            boardList.Add(new Board(2, 2, "pcbFive"));
            boardList.Add(new Board(3, 2, "pcbSix"));
            boardList.Add(new Board(1, 3, "pcbSeven"));
            boardList.Add(new Board(2, 3, "pcbEight"));
            boardList.Add(new Board(3, 3, "pcbNine"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearAllImagesFromPlayingfield();
            currentPiece = new Piece("Rook");
            currentPiece.SetLocation(1, 1);

            Bitmap bm = new Bitmap("C:\\Users\\otaku\\OneDrive\\Documentos\\School\\chesspiece.png");
            pcbOne.Image = bm;
        }

        public void GetBoardOptions()
        {
            pieceOptions = "";
            foreach (Board board in boardList)
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
                foreach (PictureBox picturebox in gbxPlayingField.Controls.OfType<PictureBox>())
                {
                    if (picturebox.Tag.ToString() == pieceOptions[i].ToString() + pieceOptions[i + 1].ToString() && picturebox.Image == null)
                    {
                        picturebox.BackColor = Color.Green;
                    }
                }
            }
        }

        public void ClearAllImagesFromPlayingfield()
        {
            foreach (PictureBox picturebox in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = Color.LightGray;
                picturebox.Image = null;
            }
        }

        public void ClearBoardcolors()
        {
            foreach (PictureBox picturebox in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = Color.LightGray;
            }
        }

        private void btnKnight_Click(object sender, EventArgs e)
        {
            ClearAllImagesFromPlayingfield();
            currentPiece = new Piece("Knight");
            currentPiece.SetLocation(1, 1);

            Bitmap bm = new Bitmap("C:\\Users\\otaku\\OneDrive\\Documentos\\School\\chesspiece_knight.png");
            pcbOne.Image = bm;
        }
    }
}
