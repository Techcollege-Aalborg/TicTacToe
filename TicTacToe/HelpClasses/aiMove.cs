using System;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    public static class AIMove
    {
        public static bool validate;
        public static string[] lookFor;
        public static int xCount;
        public static int oCount;
        public static int emptyCount;
        public static bool canWin;
        public static Control moveCon;
        public static Int32[] yxPos;
        private static string currentPlayer;

        /// <summary>
        /// Match the PlayField coordinates with the correct - Form Button Control
        /// and checks the field is empty
        /// </summary>
        /// <param name="controls">All form controls</param>
        /// <param name="pos">PlayField xypos</param>
        public static void ControlMove(Control.ControlCollection con, int[] pos)
        {
            // Set a variable with the control to make a move on
            if (pos[0] == 0 && pos[1] == 0)
                moveCon = con.Find("buttonField1", false).FirstOrDefault();
            if (pos[0] == 0 && pos[1] == 1)
                moveCon = con.Find("buttonField2", false).FirstOrDefault();
            if (pos[0] == 0 && pos[1] == 2)
                moveCon = con.Find("buttonField3", false).FirstOrDefault();
            if (pos[0] == 1 && pos[1] == 0)
                moveCon = con.Find("buttonField4", false).FirstOrDefault();
            if (pos[0] == 1 && pos[1] == 1)
                moveCon = con.Find("buttonField5", false).FirstOrDefault();
            if (pos[0] == 1 && pos[1] == 2)
                moveCon = con.Find("buttonField6", false).FirstOrDefault();
            if (pos[0] == 2 && pos[1] == 0)
                moveCon = con.Find("buttonField7", false).FirstOrDefault();
            if (pos[0] == 2 && pos[1] == 1)
                moveCon = con.Find("buttonField8", false).FirstOrDefault();
            if (pos[0] == 2 && pos[1] == 2)
                moveCon = con.Find("buttonField9", false).FirstOrDefault();

            if (moveCon.Text == "")
            {
                validate = true;
                return;
            }

            return;
        }

        /// <summary>
        /// Check to see if a player can win
        /// </summary>
        /// <param name="startX">Start X</param>
        /// <param name="startY">Start Y</param>
        /// <param name="board">PlayField Type</param>
        /// <param name="dx">Directional X</param>
        /// <param name="dy">Directional Y</param>
        /// <param name="look">What symbol to look for (X,O)</param>
        /// <returns>True/False if a opponent has a winning chance </returns>
        public static bool CheckFields(int startX, int startY, PlayField board, int dx, int dy, string look)
        {
            // Reset Values/ Initialize variables
            xCount = 0;
            oCount = 0;
            emptyCount = 0; // Is this used=???
            yxPos = new int[2];

            // Loops through the playfield
            for (var i = 0; i < 3; i++)
            {
                int y = startY + dy * i;
                int x = startX + dx * i;

                if (board.field[y, x] == "")
                {
                    yxPos[0] = y;
                    yxPos[1] = x;
                    emptyCount++; // Is this used=??
                }
                if (board.field[y, x] == "X")
                {
                    xCount++;
                }
                if (board.field[y, x] == "O")
                {
                    oCount++;
                }
            }

            if (oCount.Equals(2) && emptyCount.Equals(1) && look == "O")
            {
                canWin = true;
                return true;
            }
            if (xCount.Equals(2) && emptyCount.Equals(1) && look == "X")
            {
                canWin = true;
                return true;
            }

            canWin = false;
            return false;
        }

        /// <summary>
        /// AI Move Invoke Handler
        /// </summary>
        /// <param name="board">PlayField Type</param>
        /// <param name="controls">All form contorls</param>
        /// <param name="player">String from ENUM</param>
        public static void Move(PlayField board, Control.ControlCollection controls, string player)
        {
            CheckMove(board, controls, player);
            MakeMove(controls);
        }

        /// <summary>
        /// Checks for counter/winning moves
        /// </summary>
        /// <param name="board">PlayField Type</param>
        /// <param name="controls">All form controls</param>
        /// <param name="player">String from ENUM</param>
        public static bool CheckMove(PlayField board, Control.ControlCollection controls, string player)
        {
            currentPlayer = player;

            // Set the symbols to look for
            lookFor = new string[] { "O", "X" };

            // Goes through all symbols in order
            foreach (string s in lookFor)
            {
                // Check columns for counter/winning moves
                for (var x = 0; x < board.field.GetLength(0); x++)
                {
                    if (CheckFields(x, 0, board, 0, 1, s))
                    {
                        return true;
                    }
                }

                // Check rows for counter/winning moves
                for (var y = 0; y < board.field.GetLength(0); y++)
                {
                    if (CheckFields(0, y, board, 1, 0, s))
                    {
                        return true;
                    }
                }

                // Check diagonals for counter/winning moves
                if (CheckFields(0, 0, board, 1, 1, s))
                {
                    return true;
                }

                if (CheckFields(2, 0, board, -1, 1, s))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Makes a countermove/winning if canWin is true
        /// else makes a random move to an empty brick
        /// </summary>
        /// <param name="win">If a player can win</param>
        /// <param name="controls"></param>
        public static void MakeMove(Control.ControlCollection controls)
        {
            // Counter/Winning move
            if (canWin)
            {
                ControlMove(controls, yxPos);
                moveCon.Text = currentPlayer.ToString();
                moveCon.Enabled = false;
                return;
            }
            // Random move
            else
            {
                validate = false;
                do
                {
                    yxPos[0] = RandomNumber(0, 3);
                    yxPos[1] = RandomNumber(0, 3);
                    ControlMove(controls, yxPos);
                    if (moveCon.Text == "")
                    {
                        moveCon.Text = currentPlayer.ToString();
                        moveCon.Enabled = false;
                    }
                } while (validate == false);

                return;
            }
        }

        /// <summary>
        /// Picks a random number between the provided scope
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns></returns>
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}