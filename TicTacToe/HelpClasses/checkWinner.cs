using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class CheckWinner
    {
        /// <summary>
        /// Variable used to compare fields
        /// </summary>
        public static string firstField;

        /// <summary>
        /// Used to check if all fields are the same 
        /// in a row/ column
        /// </summary>
        /// <param name="startX">The start x pos</param>
        /// <param name="startY">The start y pos</param>
        /// <param name="board">The PlayField</param>
        /// <param name="dx">Directional X pos</param>
        /// <param name="dy">Directional Y pos</param>
        /// <returns></returns>
        public static bool AllFieldsTheSame(int startX, int startY, PlayField board, int dx, int dy)
        {
            // Set first field for comparison
            firstField = board.field[startX,startY];

            // Check first field for empty
            if (firstField == "")
            {
                return false;
            }

            // Loop through fields
            for (var i = 0; i < 3; i++)
            {
                int y = startY + dy * i;
                int x = startX + dx * i;

                // No Winner if field is not = firstfield
                if (board.field[x,y] != firstField)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Method responsible for checking for a winner
        /// in every row/ column
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
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

            // Check 1 diagonal
            if (AllFieldsTheSame(0, 0, board, 1, 1))
            {
                return firstField;
            }

            // Check 2 diagonal
            if (AllFieldsTheSame(2, 0, board, -1, 1))
            {
                return firstField;
            }

            return "";
        }
    }
}
