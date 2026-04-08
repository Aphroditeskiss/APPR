using System;

namespace APPR_ChessPiecesAsChessPieces_22SD_DaphnevanRooij
{
    internal class Location
    {
        public int Row { get; }
        public int Col { get; }
        public int RS { get; }
        public int HS { get; }
        public int VS { get; }

        public Location(int row, int col, int rs, int hs, int vs)
        {
            Row = row;
            Col = col;
            RS = rs;
            HS = hs;
            VS = vs;
        }
    }
}