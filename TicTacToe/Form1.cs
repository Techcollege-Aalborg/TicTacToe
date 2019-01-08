using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        // Globally used variables
        String PlayerOneName = "";
        String PlayerTwoName = "";
        Player CurrentPlayer;
        int MaxBricks = 6;
        int UsedBricks = 0;
        Button bt;
        bool NoBricks = false;
        bool AdvancedVersion = false;
        bool FieldEmpty;
        bool MovedBrick = false;
        bool AIEnabled = false;
        String OldPlayerName = "";

        public TicTacToe()
        {
            // Start the app
            InitializeComponent();
        }

        // Player turn 
        public enum Player
        {
            X,
            O
        }

        private void TicTacToe_Load(object sender, EventArgs e)
        {
            // Ouput a message
            AddLogEntry("Welcome to TicTacToe! There are 2 mode. " +
                "Simple and advanced! Simple uses all fields while on advanced you only get 3 bricks! Enjoy playing.");
        }

        private void PlayerOneTextChanged(object sender, EventArgs e)
        {
            // Set new playername
            PlayerOneName = textPlayerOneName.Text;

            //Control test = this.Controls.Find("textPlayerOneName", false).FirstOrDefault();
            //AddLogEntry(test.Text);
        }

        private void PlayerTwoNameChanged(object sender, EventArgs e)
        {
            // Saves old playername
            if(textPlayerTwoName.Text != "AI")
            {
                OldPlayerName = textPlayerTwoName.Text;
            }

            // Sets new playername
            PlayerTwoName = textPlayerTwoName.Text;;
        }

        private void PlayerCount_Click(object sender, EventArgs e)
        {
            bt = (Button)sender;
            PlayerCount(bt.Text);
        }

        private void PlayerCount(string players)
        {
            // Set game to against computer
            if (players == "1")
            {
                textPlayerTwoName.Text = "AI";
                textPlayerTwoName.Enabled = false;
                AIEnabled = true;
                ResetGame(true);
                AdvancedVersion = false;
            }
            // Set game to against real player
            if (players == "2")
            {
                if (textPlayerTwoName.Text == "AI")
                    textPlayerTwoName.Text = OldPlayerName;
                if (textPlayerTwoName.Enabled == false)
                    textPlayerTwoName.Enabled = true;
                ResetGame(true);
                AIEnabled = false;
            }
        }

        private void ResetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset game incl. score
            ResetGame(true);
        }

        private void ResetCurrentGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset game keeping the score
            ResetGame(false);
        }

        private void ResetScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset the score labels
            labelPlayerTwoScoreShow.Text = "0";
            labelPlayerOneScoreShow.Text = "0";
        }

        private void SimpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turn off advanced mode
            AdvancedVersion = false;
            NoBricks = false;
        }

        private void AdvancesdToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Turn on advanced mode
            AdvancedVersion = true;
            // AI cannot play advanced -> set 2 player
            PlayerCount("2");
        }

        private void ResetGame(bool AllReset = false)
        {
            // Reset score aswell
            if (AllReset == true)
            {
                labelPlayerTwoScoreShow.Text = "0";
                labelPlayerOneScoreShow.Text = "0";
                NoBricks = false;
                UsedBricks = 0;
                CurrentPlayer = Player.X;
            }
            // Keeps the score 
            else
            {
                NoBricks = false;
                UsedBricks = 0;
                CurrentPlayer = Player.X;
            }

            // Reset all button controls 
            foreach (Control con in Controls)
            {
                if (con is Button)
                {
                    if (con.Text == "1" || con.Text == "2")//(con.Name == "buttonPlayerOne" || con.Text == "buttonPlayerTwo") Did not work
                        continue;
                    else
                    {
                        con.Text = "";
                        con.Enabled = true;
                    }  
                }
            }
        }

        public void GameFieldClick(object sender, EventArgs e)
        {
           bt = (Button)sender;

           if (AdvancedVersion == true)
            {
                // Check if max bricks have been used.
                CheckMaxBricks();
                // Check field value -> goes to place a brick -> finally check for winner.
                CheckFieldValue();
                PlaceBrick();
                CheckForWinner();
            }
           else
            {
                // Checks for the value of the field -> goes to place a brick.
                CheckFieldValue();
                PlaceBrick();

                // Gets the playfield
                ExtractDataOfField();

                // If AI is enabled this starts the AI's move.
                if (AIEnabled == true && UsedBricks != 9)
                {
                    AIMoves();
                }
                
                // Check for winner -> draw.
                CheckForWinner();
                CheckForDraw();
            }
        }

        private void PlaceBrick()
        {
            // Jumps to movebrick if all are used
            if (NoBricks == true)
            {
                if (FieldEmpty == true)
                {
                    AddLogEntry("You are not out of bricks. Click on a brick to move.");
                    return;
                }
                else
                {
                    MoveBrick();
                }
            }
            // Places a brick based on who turn it is
            else
            {
                // Checks if the field is empty. If not return without action
                if (FieldEmpty == false)
                {
                    AddLogEntry("This field is taken! Place click on an empty field.");
                    return;
                }

                // Place a brick based on turn
                bt.Text = CurrentPlayer.ToString();

                // Next player
                CurrentPlayer = EnumLook.Next(CurrentPlayer);
                UsedBricks++;

                // If simple version is on - disables the button
                if (AdvancedVersion == false)
                {
                    bt.Enabled = false;
                }
            }

        }

        private void CheckFieldValue()
        {
            // Checks if the field is empty
            if (bt.Text == "")
            {
                FieldEmpty = true;
            }
            else if (!(bt.Text == ""))
            {
                FieldEmpty = false;
            }
        }

        private void CheckMaxBricks()
        {
            // Checks if all bricks have been used
            if (UsedBricks == MaxBricks)
                NoBricks = true;
            else
                NoBricks = false;
        }

        private void MoveBrick()
        {
            // If turn and brick match, remove the brick
            if (CurrentPlayer.ToString() == bt.Text)
            {
                bt.Text = "";
                MovedBrick = true;
            }

            // If a brick was moved -> add a move 
            if (MovedBrick == true)
            {
                UsedBricks--;
            }
        }

        public void AddLogEntry(String Message = "")
        {
            // Append text to the textbox
            textStatus.AppendText("\n"+Message+"\n");
        }

        private void CheckForWinner()
        {
            // Check for a winner
            string Result = CheckWinner.SomeoneWins(ExtractDataOfField());

            // Display winner
            if (Result != "")
            {
                var cons = this.Controls;
                WinnerHandler.EvaluateWinner(cons, Result);
                ResetGame();
            }
        }

        private void CheckForDraw()
        {
            // If UsedBricks is 9 then the 3x3 field is filled and needs a reset.
            if (UsedBricks == 9)
            {
                AddLogEntry("It's a draw! Try again!");

                foreach (Control con in this.Controls)
                {
                    if (con is Button && !con.Enabled)
                    {
                        con.Text = "";
                        con.Enabled = true;
                        CurrentPlayer = Player.X;
                    }
                }

                UsedBricks = 0;
            }
        }

        public void AIMoves()
        {
            // Call the function to make an AI move.
            AIMove.Move(ExtractDataOfField(), this.Controls, CurrentPlayer.ToString());

            // Makes the neccessary changes to the local values.
            CurrentPlayer = EnumLook.Next(CurrentPlayer);
            UsedBricks++;
        }
        
        public PlayField ExtractDataOfField()
        {
            var controls = this.Controls;
            var field = new PlayField(controls);
            return field;
        }
    }
}
