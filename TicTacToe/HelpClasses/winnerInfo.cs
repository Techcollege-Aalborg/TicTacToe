using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class winnerInfo
    {
        public string winnerType;
        public string winnerName;
        public bool isWinner;
        public int xStart;
        public int yStart;
        public int xDir;
        public int yDir;

        public winnerInfo()
        {
            winnerType = "";
            winnerName = "";
            isWinner = false;
        }

    }
}
