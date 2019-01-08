using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public static class AIMove
    {
        public static bool canPlayerWin;
        public static bool canAIWin;
        public static bool validate;
        public static string[] lookFor;
        public static int xCount;
        public static int oCount;
        public static int emptyCount;
        public static bool canWin;
        public static Control moveCon;
        public static Int32[] yxPos;
        private static string CurrentPlayer;

        public static void ControlMove(Control.ControlCollection controls, int[] pos)
        {
            // Set a variable with the control to make a move on
            if (pos[0] == 0 && pos[1] == 0)
                moveCon = controls[20];
            if (pos[0] == 0 && pos[1] == 1)
                moveCon = controls[19];
            if (pos[0] == 0 && pos[1] == 2)
                moveCon = controls[18];
            if (pos[0] == 1 && pos[1] == 0)
                moveCon = controls[17];
            if (pos[0] == 1 && pos[1] == 1)
                moveCon = controls[16];
            if (pos[0] == 1 && pos[1] == 2)
                moveCon = controls[15];
            if (pos[0] == 2 && pos[1] == 0)
                moveCon = controls[14];
            if (pos[0] == 2 && pos[1] == 1)
                moveCon = controls[13];
            if (pos[0] == 2 && pos[1] == 2)
                moveCon = controls[12];

            if (moveCon.Text == "")
            {
                validate = true;
                return;
            }
            
            return;
        }

        public static bool CheckFields(int startX, int startY, PlayField board, int dx, int dy, string look)
        {
            xCount = 0;
            oCount = 0;
            emptyCount = 0;
            yxPos = new int[2];

            for (var i = 0; i < 3; i++)
            {
                int y = startY + dy * i;
                int x = startX + dx * i;

                if (board.field[y, x] == "")
                {
                    yxPos[0] = y;
                    yxPos[1] = x;
                    emptyCount++;
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

        public static void Move(PlayField board, Control.ControlCollection controls, string player)
        {
            CurrentPlayer = player;
            lookFor = new string[] { "O", "X" };

            // Check columns for winning moves
            foreach (string s in lookFor)
            {
                for (var x = 0; x < board.field.GetLength(0); x++)
                {
                    if (CheckFields(x, 0, board, 0, 1, s))
                    {
                        MakeMove(canWin, controls);
                        return;
                    }
                }

                // Check rows for winning moves
                for (var y = 0; y < board.field.GetLength(0); y++)
                {
                    if (CheckFields(0, y, board, 1, 0, s))
                    {
                        MakeMove(canWin, controls);
                        return;
                    }
                }

                // Check diagonals for winning moves
                if (CheckFields(0, 0, board, 1, 1, s))
                {
                    // move(controls);
                    MakeMove(canWin, controls);
                    return;
                }

                if (CheckFields(2, 0, board, -1, 1, s))
                {
                    // move(controls);
                    MakeMove(canWin, controls);
                    return;
                }
            }

            MakeMove(canWin, controls);
            return;
        }

        public static void MakeMove(bool win, Control.ControlCollection controls)
        {
            if (win)
            {
                ControlMove(controls, yxPos);
                moveCon.Text = CurrentPlayer.ToString();
                moveCon.Enabled = false;
                return;
            }
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
                        moveCon.Text = CurrentPlayer.ToString();
                        moveCon.Enabled = false;
                    }
                } while (validate == false);

                return;
            }
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
