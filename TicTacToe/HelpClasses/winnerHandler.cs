using System;
using System.Linq;
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

        /// <summary>
        /// Display the winner and update the score
        /// </summary>
        /// <param name="winnerLabel">Score Label</param>
        /// <param name="winnerNameBox">Textbox containing the name of the player</param>
        /// <param name="winner"></param>
        private static void DisplayWinner(Control winnerLabel, Control winnerNameBox, string winner)
        {
            // Set the winner label
            winnerLabel.Text = (Int32.Parse(winnerLabel.Text) + 1).ToString();

            // Winner popup box
            MessageBox.Show("The winner is " + winner, "Winner Winner Chicken Dinner!");

            return;
        }

        /// <summary>
        /// Used to set correct variables based on the winner
        /// then calls to set a winner
        /// </summary>
        /// <param name="con">All controls in the form</param>
        /// <param name="winner"></param>
        public static void EvaluateWinner(Control.ControlCollection con, string winner) // New name??
        {
            // Gets the controls
            playerOneName = con.Find("textPlayerOneName", false).FirstOrDefault();
            playerTwoName = con.Find("textPlayerTwoName", false).FirstOrDefault();
            playerOneLabel = con.Find("labelPlayerOneScoreShow", false).FirstOrDefault();
            playerTwoLabel = con.Find("labelPlayerTwoScoreShow", false).FirstOrDefault();

            // Based on winner X,O
            if (winner == "X")
            {
                if (playerOneName.Text == "")
                {
                    winnerName = winner;
                }
                else
                {
                    winnerName = playerOneName.Text;
                }
                DisplayWinner(playerOneLabel, playerOneName, winnerName);
            }
            if (winner == "O")
            {
                if (playerTwoName.Text == "")
                {
                    winnerName = winner;
                }
                else
                {
                    winnerName = playerTwoName.Text;
                }
                DisplayWinner(playerTwoLabel, playerTwoName, winnerName);
            }
        }
    }
}