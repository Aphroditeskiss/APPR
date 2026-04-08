using System;

namespace APPR_ChessPiecesAsChessPieces_22SD_DaphnevanRooij
{
    internal class Piece
    {
        public string Name { get; private set; }
        public string Color { get; private set; }
        public int Horizontal { get; private set; }
        public int Vertical { get; private set; }

        private string moveOptions;

        public Piece(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public void SetLocation(int h, int v)
        {
            Horizontal = h;
            Vertical = v;
        }

        public string GetMoveoptions(int targetHor, int targetVer)
        {
            if (targetHor == Horizontal && targetVer == Vertical)
                return ""; // can't move to same square

            moveOptions = "";
            int diffHor = Math.Abs(targetHor - Horizontal);
            int diffVer = Math.Abs(targetVer - Vertical);

            switch (Name) //different rules for each piece
            {
                case "Rook":
                    if ((diffHor == 0 && diffVer != 0) || (diffVer == 0 && diffHor != 0))
                        moveOptions = $"{targetHor}{targetVer}";
                    break;

                case "Knight":
                    if ((diffHor == 2 && diffVer == 1) || (diffHor == 1 && diffVer == 2))
                        moveOptions = $"{targetHor}{targetVer}";
                    break;

                case "Queen":
                    bool isStraight = (diffHor == 0 && diffVer != 0) || (diffVer == 0 && diffHor != 0);
                    bool isDiagonal = (diffHor == diffVer && diffHor != 0);
                    if (isStraight || isDiagonal)
                        moveOptions = $"{targetHor}{targetVer}";
                    break;
                case "King":
                    bool kingStraight = (diffHor == 0 && diffVer == 1) ||
                        (diffVer == 0 && diffHor == 1);

                    bool kingDiagonal = (diffHor == 1 && diffVer == 1);
                    if (kingStraight || kingDiagonal)
                        moveOptions = $"{targetHor}{targetVer}";
                    break;
                case "Wizard":
                    if (!(diffHor == 0 && diffVer == 0))
                        moveOptions = $"{targetHor}{targetVer}";
                    break;
            }
            return moveOptions;
        }

    }
}