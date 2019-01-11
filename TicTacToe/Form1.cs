﻿using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        // Globally used variables
        private String PlayerOneName = "";

        private String PlayerTwoName = "";
        private Player CurrentPlayer;
        private int MaxBricks = 6;
        private int UsedBricks = 0;
        private Button bt;
        private bool NoBricks = false;
        private bool AdvancedVersion = false;
        private bool FieldEmpty;
        private bool MovedBrick = false;
        private bool AIEnabled = false;
        private String OldPlayerName = "";

        /// <summary>
        /// Start the game
        /// </summary>
        public TicTacToe()
        {
            // Start the app
            InitializeComponent();
        }

        /// <summary>
        /// Used to set a player
        /// </summary>
        public enum Player
        {
            X,
            O
        }

        /// <summary>
        /// Display a message on form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TicTacToe_Load(object sender, EventArgs e)
        {
            // Ouput a message
            AddLogEntry("Welcome to TicTacToe! There are 2 mode. " +
                "Simple and advanced! Simple uses all fields while on advanced you only get 3 bricks! Enjoy playing.");
        }

        /// <summary>
        /// Change Player name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerOneTextChanged(object sender, EventArgs e)
        {
            // Set new playername
            PlayerOneName = textPlayerOneName.Text;
        }

        /// <summary>
        /// Change player name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerTwoNameChanged(object sender, EventArgs e)
        {
            // Saves old playername
            if (textPlayerTwoName.Text != "AI")
            {
                OldPlayerName = textPlayerTwoName.Text;
            }

            // Sets new playername
            PlayerTwoName = textPlayerTwoName.Text; ;
        }

        /// <summary>
        /// Eventhandler for when one of the buttons to choose player count is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerCount_Click(object sender, EventArgs e)
        {
            bt = (Button)sender;
            PlayerCount(bt.Text);
        }

        /// <summary>
        /// Changes player count when called
        /// </summary>
        /// <param name="players">Number of players (1,2)</param>
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

        /// <summary>
        /// Eventhandler for ResetGme button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset game incl. score
            ResetGame(true);
        }

        /// <summary>
        /// Eventhandler for ResetCurrentGame button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetCurrentGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset game keeping the score
            ResetGame(false);
        }

        /// <summary>
        /// Eventhandler for ResetScore button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reset the score labels
            labelPlayerTwoScoreShow.Text = "0";
            labelPlayerOneScoreShow.Text = "0";
        }

        /// <summary>
        /// Disables advanced mode switching to simple
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Turn off advanced mode
            AdvancedVersion = false;
            NoBricks = false;
        }

        /// <summary>
        /// Enable advanced game mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvancesdToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Turn on advanced mode
            AdvancedVersion = true;
            // AI cannot play advanced -> set 2 player
            PlayerCount("2");
        }

        /// <summary>
        /// Resets the game
        /// </summary>
        /// <param name="AllReset"></param>
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

        /// <summary>
        /// Eventhandler for click on any gamefields
        /// This goes is the "main" line for progression in the game.
        /// This handler only calls methods in correct order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// This is the handler that will "place" a brick on the playfield
        /// It will run thruogh the nessasary checks before placing
        /// </summary>
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

        /// <summary>
        /// Check if the field is empty
        /// Sets a variable to true/false based on this
        /// </summary>
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

        /// <summary>
        /// Check if all available bricks have been used
        /// Sets a variable to true/false based on this
        /// </summary>
        private void CheckMaxBricks()
        {
            // Checks if all bricks have been used
            if (UsedBricks == MaxBricks)
                NoBricks = true;
            else
                NoBricks = false;
        }

        /// <summary>
        /// This handles the effect of "moving" a brick
        /// This method does NOT place a brick - it removes the brick and adds a move to the game
        /// </summary>
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

        /// <summary>
        /// Output a message to the textbox control
        /// </summary>
        /// <param name="Message">Message to be displayed</param>
        public void AddLogEntry(String Message = "")
        {
            // Append text to the textbox
            textStatus.AppendText("\n" + Message + "\n");
        }

        /// <summary>
        /// This handler is responsible for checking for a winner
        /// Uses a HelpClass
        /// </summary>
        private void CheckForWinner()
        {
            // Check for a winner
            string winner = CheckWinner.SomeoneWins(ExtractDataOfField());

            // Display winner
            if (winner != "")
            {
                var con = this.Controls;
                WinnerHandler.EvaluateWinner(con, winner);
                ResetGame();
            }
        }

        /// <summary>
        /// Handler responsible for checking for draw
        /// Does this by checking the value of UsedBricks variable
        /// </summary>
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

        /// <summary>
        /// AI handler - responsible for the "AI" moves
        /// Uses a HelpClass for this
        /// </summary>
        public void AIMoves()
        {
            // Call the function to make an AI move.
            AIMove.Move(ExtractDataOfField(), this.Controls, CurrentPlayer.ToString());

            // Makes the neccessary changes to the local values.
            CurrentPlayer = EnumLook.Next(CurrentPlayer);
            UsedBricks++;
        }

        /// <summary>
        /// Used to extract data into a PlayField class
        /// Uses a HelpClass/ Custom Data Type
        /// </summary>
        /// <returns>A playfield [,] - Populated with strings</returns>
        public PlayField ExtractDataOfField()
        {
            var controls = this.Controls;
            var field = new PlayField(controls);
            return field;
        }
    }
}