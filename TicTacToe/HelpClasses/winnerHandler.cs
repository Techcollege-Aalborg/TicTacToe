using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public static class WinnerHandler
    {
        public static string winnerName;
        public static int Score;
        public static Control playerOneName;
        public static Control playerTwoName;
        public static Control playerOneLabel;
        public static Control playerTwoLabel;

        private  static void SetWinner(Control winnerLabel, Control winnerNameBox, string s)
        {
            // Set the winner label
            winnerLabel.Text = (Int32.Parse(winnerLabel.Text) + 1).ToString();

            // Winner popup box
            MessageBox.Show("The winner is " + s, "Winner Winner Chicken Dinner!");

            return;
        }
        public static void EvaluateWinner(Control.ControlCollection con, string s)
        {
            // Gets the controls
            playerOneName = con.Find("textPlayerOneName", false).FirstOrDefault();
            playerTwoName = con.Find("textPlayerTwoName", false).FirstOrDefault();
            playerOneLabel = con.Find("labelPlayerOneScoreShow", false).FirstOrDefault();
            playerTwoLabel = con.Find("labelPlayerTwoScoreShow", false).FirstOrDefault();

            // Based on winner X,O 
            if (s == "X")
            {
                if (playerOneName.Text == "")
                {
                    winnerName = s;
                }
                else
                {
                    winnerName = playerOneName.Text;
                }
                SetWinner(playerOneLabel, playerOneName, winnerName);
            }
            if (s == "O")
            {
                if (playerTwoName.Text == "")
                {
                    winnerName = s;
                }
                else
                {
                    winnerName = playerTwoName.Text;
                }
                SetWinner(playerTwoLabel, playerTwoName, winnerName);
            }

        }
    }
}
