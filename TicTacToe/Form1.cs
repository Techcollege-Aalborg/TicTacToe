using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        // Globally used variables
        private String playerOneName = "";
        private String playerTwoName = "";
        private Player currentPlayer;
        private int maxBricks = 6;
        private int usedBricks = 0;
        private Button bt;
        private bool noBricks = false;
        private bool advancedVersion = false;
        private bool fieldEmpty;
        private bool movedBrick = false;
        private bool aIEnabled = false;
        private String oldPlayerName = "";

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
            playerOneName = textPlayerOneName.Text;
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
                oldPlayerName = textPlayerTwoName.Text;
            }

            // Sets new playername
            playerTwoName = textPlayerTwoName.Text; ;
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
                aIEnabled = true;
                ResetGame(true);
                advancedVersion = false;
            }
            // Set game to against real player
            if (players == "2")
            {
                if (textPlayerTwoName.Text == "AI")
                    textPlayerTwoName.Text = oldPlayerName;
                if (textPlayerTwoName.Enabled == false)
                    textPlayerTwoName.Enabled = true;
                ResetGame(true);
                aIEnabled = false;
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
            advancedVersion = false;
            noBricks = false;
        }

        /// <summary>
        /// Enable advanced game mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvancesdToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Turn on advanced mode
            advancedVersion = true;
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
                noBricks = false;
                usedBricks = 0;
                currentPlayer = Player.X;
            }
            // Keeps the score
            else
            {
                noBricks = false;
                usedBricks = 0;
                currentPlayer = Player.X;
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

            if (advancedVersion == true)
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
                if (aIEnabled == true && usedBricks != 9)
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
            // Handles if no bricks left
            if (noBricks == true)
            {
                if (fieldEmpty == true)
                {
                    AddLogEntry("You are not out of bricks. Click on a brick to move.");
                    return;
                }
                else
                {
                    RemoveBrick();
                }
            }
            // Places a brick based on who turn it is
            else
            {
                // Checks if the field is empty. If not return without action
                if (fieldEmpty == false)
                {
                    AddLogEntry("This field is taken! Place click on an empty field.");
                    return;
                }

                // Place a brick based on turn
                bt.Text = currentPlayer.ToString();

                // Next player
                currentPlayer = EnumLook.Next(currentPlayer);
                usedBricks++;

                // If simple version is on - disables the button
                if (advancedVersion == false)
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
                fieldEmpty = true;
            }
            else if (!(bt.Text == ""))
            {
                fieldEmpty = false;
            }
        }

        /// <summary>
        /// Check if all available bricks have been used
        /// Sets a variable to true/false based on this
        /// </summary>
        private void CheckMaxBricks()
        {
            // Checks if all bricks have been used
            if (usedBricks == maxBricks)
                noBricks = true;
            else
                noBricks = false;
        }

        /// <summary>
        /// This handles the removal of a brick.
        /// And adds a move to the game.
        /// </summary>
        private void RemoveBrick()
        {
            // If turn and brick match, remove the brick
            if (currentPlayer.ToString() == bt.Text)
            {
                bt.Text = "";
                movedBrick = true;
            }

            // If a brick was moved -> add a move
            if (movedBrick == true)
            {
                usedBricks--;
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
        /// This handler is responsible for checking for a winner.
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
        /// Handler responsible for checking for draw.
        /// Does this by checking the value of UsedBricks variable
        /// </summary>
        private void CheckForDraw()
        {
            // If UsedBricks is 9 then the 3x3 field is filled and needs a reset.
            if (usedBricks == 9)
            {
                AddLogEntry("It's a draw! Try again!");

                foreach (Control con in this.Controls)
                {
                    if (con is Button && !con.Enabled)
                    {
                        con.Text = "";
                        con.Enabled = true;
                        currentPlayer = Player.X;
                    }
                }

                usedBricks = 0;
            }
        }

        /// <summary>
        /// AI handler - responsible for the "AI" moves.
        /// Uses a HelpClass for this
        /// </summary>
        public void AIMoves()
        {
            // Call the function to make an AI move.
            AIMove.Move(ExtractDataOfField(), this.Controls, currentPlayer.ToString());

            // Makes the neccessary changes to the local values.
            currentPlayer = EnumLook.Next(currentPlayer);
            usedBricks++;
        }

        /// <summary>
        /// Used to extract data into a PlayField class.
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