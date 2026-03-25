using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPR_WhatPieceDidYouMove_24SD_DaphnevanRooij
{
    internal class Piece
    {
        private string name = "";
        private string moveOptions = "";
        private int curHor, curVer, newHor, newVer;

        public Piece(string c_name)
        {
            name = c_name;
        }

        public void SetLocation(int _newHor, int _newVer)
        {
            curHor = _newHor;
            curVer = _newVer;
        }

        public string GetMoveoptions(int _newHor, int _newVer)
        {
            newHor = _newHor;
            newVer = _newVer;
            moveOptions = "";

            switch (name)
            {
                case "Leon": MoveLeon(); break;
                case "Swain": MoveSwain(); break;
                default:
                    break;
            }
            return moveOptions;
        }

        public void MoveLeon()
        {
            int temp_hor = Math.Abs(newHor - curHor);
            int temp_ver = Math.Abs(newVer - curVer);

            if (temp_ver == 1)
            {
                if (temp_hor == 0)
                {
                    moveOptions = $"{newHor}{newVer}";
                }
            }
            else if (temp_hor == 1)
            {
                if (temp_ver == 0)
                {
                    moveOptions = $"{newHor}{newVer}";
                }
            }
        }

        public void MoveSwain()
        {
            moveOptions = $"{newHor}{newVer}";
        }

        public int GetCurrentHorizontal() { return curHor; }
        public int GetCurrentVertical()
        {
            return curVer;
        }
    }
}