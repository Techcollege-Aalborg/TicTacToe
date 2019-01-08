using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class CheckWinner
    {
        public static string firstField;

        public static bool AllFieldsTheSame(int startX, int startY, PlayField board, int dx, int dy)
        {
            firstField = board.field[startY,startX];
            if (firstField == "")
            {
                return false;
            }

            for (var i = 0; i < 3; i++)
            {
                int y = startY + dy * i;
                int x = startX + dx * i;
                if (board.field[y,x] != firstField)
                {
                    return false;
                }
            }

            return true;
        }
        public static string SomeoneWins(PlayField board)
        {
            // Check columns
            for (var x = 0; x < board.field.GetLength(0); x++)
            {
                if (AllFieldsTheSame(x, 0, board, 0, 1))
                {
                    return firstField;
                }     
            }

            // Check rows
            for (var y = 0; y < board.field.GetLength(0); y++)
            {

                if (AllFieldsTheSame(0, y, board, 1, 0))
                {
                    return firstField;
                }
                    
            }

            // Check diagonals
            if (AllFieldsTheSame(0, 0, board, 1, 1))
            {
                return firstField;
            }
                

            if (AllFieldsTheSame(2, 0, board, -1, 1))
            {
                return firstField;
            }

            return "";
        }
    }
}
