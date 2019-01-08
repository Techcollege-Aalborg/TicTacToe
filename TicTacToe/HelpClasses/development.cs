using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class Development
    {
        public static string Test()
        {
            return "test";
        }
        
        public static string DicideWinner(PlayField g)
        {
            // Check every row
            if (g.field[0, 0] == g.field[0, 1] && g.field[0, 1] == g.field[0, 2] && g.field[0, 2] == "X") return "X";
            if (g.field[1, 0] == g.field[1, 1] && g.field[1, 1] == g.field[1, 2] && g.field[1, 2] == "X") return "X";
            if (g.field[2, 0] == g.field[2, 1] && g.field[2, 1] == g.field[2, 2] && g.field[2, 2] == "X") return "X";

            if (g.field[0, 0] == g.field[0, 1] && g.field[0, 1] == g.field[0, 2] && g.field[0, 2] == "O") return "O";
            if (g.field[1, 0] == g.field[1, 1] && g.field[1, 1] == g.field[1, 2] && g.field[1, 2] == "O") return "O";
            if (g.field[2, 0] == g.field[2, 1] && g.field[2, 1] == g.field[2, 2] && g.field[2, 2] == "O") return "O";

            // Check every colum
            if (g.field[0, 0] == g.field[1, 0] && g.field[1, 0] == g.field[2, 0] && g.field[2, 0] == "X") return "X";
            if (g.field[0, 1] == g.field[1, 1] && g.field[1, 1] == g.field[2, 1] && g.field[2, 1] == "X") return "X";
            if (g.field[0, 2] == g.field[1, 2] && g.field[1, 2] == g.field[2, 2] && g.field[2, 2] == "X") return "X";

            if (g.field[0, 0] == g.field[1, 0] && g.field[1, 0] == g.field[2, 0] && g.field[2, 0] == "O") return "O";
            if (g.field[0, 1] == g.field[1, 1] && g.field[1, 1] == g.field[2, 1] && g.field[2, 1] == "O") return "O";
            if (g.field[0, 2] == g.field[1, 2] && g.field[1, 2] == g.field[2, 2] && g.field[2, 2] == "O") return "O";

            // Chech diagnally                               
            if (g.field[0, 0] == g.field[1, 1] && g.field[1, 1] == g.field[2, 2] && g.field[2, 2] == "X") return "X";
            if (g.field[2, 0] == g.field[1, 1] && g.field[1, 1] == g.field[2, 0] && g.field[2, 0] == "X") return "X";

            if (g.field[0, 0] == g.field[1, 1] && g.field[1, 1] == g.field[2, 2] && g.field[2, 2] == "O") return "O";
            if (g.field[2, 0] == g.field[1, 1] && g.field[1, 1] == g.field[2, 0] && g.field[2, 0] == "O") return "O";

            return "";
        }

        public static string AIDicision(PlayField g)
        {

            String button = "";

            // Check for winning first
            // Checks every row from left to right, both X and O
            if (g.field[0, 0] == g.field[0, 1] && g.field[0, 1] == "O" && g.field[0, 2] == "") button = "3";
            if (g.field[1, 0] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[1, 2] == "") button = "6";
            if (g.field[2, 0] == g.field[2, 1] && g.field[2, 1] == "O" && g.field[2, 2] == "") button = "9";

            // if (g.field1 == g.field2 && g.field2 == "O" && g.field3 == "") button = "3";
            // if (g.field4 == g.field5 && g.field5 == "O" && g.field6 == "") button = "6";
            // if (g.field7 == g.field8 && g.field8 == "O" && g.field9 == "") button = "9";

            // Checks every row from right to left, both X and O
            if (g.field[0, 2] == g.field[0, 1] && g.field[0, 1] == "O" && g.field[0, 0] == "") button = "1";
            if (g.field[1, 2] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[1, 0] == "") button = "4";
            if (g.field[2, 2] == g.field[2, 1] && g.field[2, 1] == "O" && g.field[2, 0] == "") button = "7";

            // if (g.field3 == g.field2 && g.field3 == "O" && g.field1 == "") button = "1";
            // if (g.field6 == g.field5 && g.field6 == "O" && g.field4 == "") button = "4";
            // if (g.field9 == g.field8 && g.field9 == "O" && g.field7 == "") button = "7";

            // Check every row for side match, both X and O
            if (g.field[0, 0] == g.field[0, 2] && g.field[0, 2] == "O" && g.field[0, 1] == "") button = "2";
            if (g.field[1, 0] == g.field[1, 2] && g.field[1, 2] == "O" && g.field[1, 1] == "") button = "5";
            if (g.field[2, 0] == g.field[2, 2] && g.field[2, 2] == "O" && g.field[2, 1] == "") button = "8";

            // if (g.field[0, 0] == g.field[0, 2] && g.field[0, 2] == "O" && g.field[0, 1] == "") button = "2";
            // if (g.field[1, 0] == g.field[1, 2] && g.field[1, 2] == "O" && g.field[1, 1] == "") button = "5";
            // if (g.field[2, 0] == g.field[2, 2] && g.field[2, 2] == "O" && g.field[2, 1] == "") button = "8";

            // Check every colum from top to bottom, both X and O                       
            if (g.field[0, 0] == g.field[1, 0] && g.field[1, 0] == "O" && g.field[2, 0] == "") button = "7";
            if (g.field[0, 1] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[2, 2] == "") button = "8";
            if (g.field[0, 2] == g.field[1, 2] && g.field[1, 2] == "O" && g.field[2, 2] == "") button = "9";

            // if (g.field1 == g.field4 && g.field4 == "O" && g.field7 == "") button = "7";
            // if (g.field2 == g.field5 && g.field5 == "O" && g.field8 == "") button = "8";
            // if (g.field3 == g.field6 && g.field6 == "O" && g.field9 == "") button = "9";

            // Check every colum from bottom to top, both X and O  
            if (g.field[2, 0] == g.field[1, 0] && g.field[1, 0] == "O" && g.field[0, 0] == "") button = "1";
            if (g.field[2, 1] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[0, 1] == "") button = "2";
            if (g.field[2, 2] == g.field[1, 2] && g.field[1, 2] == "O" && g.field[0, 2] == "") button = "3";

            // if (g.field7 == g.field4 && g.field4 == "O" && g.field1 == "") button = "1";
            // if (g.field8 == g.field5 && g.field5 == "O" && g.field2 == "") button = "2";
            // if (g.field9 == g.field6 && g.field6 == "O" && g.field3 == "") button = "3";

            // Check every colum for side match, both X and O
            if (g.field[0, 0] == g.field[2, 0] && g.field[2, 0] == "O" && g.field[1, 0] == "") button = "4";
            if (g.field[0, 1] == g.field[2, 1] && g.field[2, 1] == "O" && g.field[1, 1] == "") button = "5";
            if (g.field[0, 2] == g.field[2, 2] && g.field[2, 2] == "O" && g.field[1, 2] == "") button = "6";

            // if (g.field[0, 0] == g.field[2, 0] && g.field[2, 0] == "O" && g.field[1, 1] == "") button = "4";
            // if (g.field[0, 1] == g.field[2, 1] && g.field[2, 1] == "O" && g.field[1, 1] == "") button = "5";
            // if (g.field[0, 2] == g.field[2, 2] && g.field[2, 2] == "O" && g.field[1, 1] == "") button = "6";

            // Chech diagnally from top to bottom, both X and O  
            if (g.field[0, 0] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[2, 2] == "") button = "9";
            if (g.field[0, 2] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[2, 0] == "") button = "7";

            // if (g.field1 == g.field5 && g.field5 == "O" && g.field9 == "") button = "9";
            // if (g.field3 == g.field5 && g.field5 == "O" && g.field7 == "") button = "7";

            // Chech diagnally from bottom to top, both X and O 
            if (g.field[2, 0] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[0, 2] == "") button = "3";
            if (g.field[2, 2] == g.field[1, 1] && g.field[1, 1] == "O" && g.field[0, 0] == "") button = "1";

            // if (g.field7 == g.field5 && g.field5 == "O" && g.field3 == "") button = "3";
            // if (g.field9 == g.field5 && g.field5 == "O" && g.field1 == "") button = "1";

            // if none of the above (winning moves) can be made - see if enemy can win
            if (button == "")
            {
                // Checks every row from left to right, both X and O
                if (g.field[0, 0] == g.field[0, 1] && g.field[0, 1] == "X" && g.field[0, 2] == "") button = "3";
                if (g.field[1, 0] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[1, 2] == "") button = "6";
                if (g.field[2, 0] == g.field[2, 1] && g.field[2, 1] == "X" && g.field[2, 2] == "") button = "9";

                // if (g.field1 == g.field2 && g.field2 == "O" && g.field3 == "") button = "3";
                // if (g.field4 == g.field5 && g.field5 == "O" && g.field6 == "") button = "6";
                // if (g.field7 == g.field8 && g.field8 == "O" && g.field9 == "") button = "9";

                // Checks every row from right to left, both X and O
                if (g.field[0, 2] == g.field[0, 1] && g.field[0, 1] == "X" && g.field[0, 0] == "") button = "1";
                if (g.field[1, 2] == g.field[1, 1] && g.field[1, 1] == "X" && (g.field[1, 0]) == "") button = "4";
                if (g.field[2, 2] == g.field[2, 1] && g.field[2, 1] == "X" && g.field[2, 0] == "") button = "7";

                // if (g.field3 == g.field2 && g.field3 == "O" && g.field1 == "") button = "1";
                // if (g.field6 == g.field5 && g.field6 == "O" && g.field4 == "") button = "4";
                // if (g.field9 == g.field8 && g.field9 == "O" && g.field7 == "") button = "7";

                // Check every row for side match, both X and O
                if (g.field[0, 0] == g.field[0, 2] && g.field[0, 2] == "X" && g.field[0, 1] == "") button = "2";
                if (g.field[1, 0] == g.field[1, 2] && g.field[1, 2] == "X" && g.field[1, 1] == "") button = "5";
                if (g.field[2, 0] == g.field[2, 2] && g.field[2, 2] == "X" && g.field[2, 1] == "") button = "8";

                // if (g.field[0, 0] == g.field[0, 2] && g.field[0, 2] == "O" && g.field[0, 1] == "") button = "2";
                // if (g.field[1, 0] == g.field[1, 2] && g.field[1, 2] == "O" && g.field[1, 1] == "") button = "5";
                // if (g.field[2, 0] == g.field[2, 2] && g.field[2, 2] == "O" && g.field[2, 1] == "") button = "8";

                // Check every colum from top to bottom, both X and O                       
                if (g.field[0, 0] == g.field[1, 0] && g.field[1, 0] == "X" && g.field[2, 0] == "") button = "7";
                if (g.field[0, 1] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[2, 2] == "") button = "8";
                if (g.field[0, 2] == g.field[1, 2] && g.field[1, 2] == "X" && g.field[2, 2] == "") button = "9";

                // if (g.field1 == g.field4 && g.field4 == "O" && g.field7 == "") button = "7";
                // if (g.field2 == g.field5 && g.field5 == "O" && g.field8 == "") button = "8";
                // if (g.field3 == g.field6 && g.field6 == "O" && g.field9 == "") button = "9";

                // Check every colum from bottom to top, both X and O  
                if (g.field[2, 0] == g.field[1, 0] && g.field[1, 0] == "X" && g.field[0, 0] == "") button = "1";
                if (g.field[2, 1] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[0, 1] == "") button = "2";
                if (g.field[2, 2] == g.field[1, 2] && g.field[1, 2] == "X" && g.field[0, 2] == "") button = "3";

                // if (g.field7 == g.field4 && g.field4 == "O" && g.field1 == "") button = "1";
                // if (g.field8 == g.field5 && g.field5 == "O" && g.field2 == "") button = "2";
                // if (g.field9 == g.field6 && g.field6 == "O" && g.field3 == "") button = "3";

                // Check every colum for side match, both X and O
                if (g.field[0, 0] == g.field[2, 0] && g.field[2, 0] == "X" && g.field[1, 0] == "") button = "4";
                if (g.field[0, 1] == g.field[2, 1] && g.field[2, 1] == "X" && g.field[1, 1] == "") button = "5";
                if (g.field[0, 2] == g.field[2, 2] && g.field[2, 2] == "X" && g.field[1, 2] == "") button = "6";

                // if (g.field[0, 0] == g.field[2, 0] && g.field[2, 0] == "O" && g.field[1, 1] == "") button = "4";
                // if (g.field[0, 1] == g.field[2, 1] && g.field[2, 1] == "O" && g.field[1, 1] == "") button = "5";
                // if (g.field[0, 2] == g.field[2, 2] && g.field[2, 2] == "O" && g.field[1, 1] == "") button = "6";

                // Chech diagnally from top to bottom, both X and O  
                if (g.field[0, 0] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[2, 2] == "") button = "9";
                if (g.field[0, 2] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[2, 0] == "") button = "7";

                // if (g.field1 == g.field5 && g.field5 == "O" && g.field9 == "") button = "9";
                // if (g.field3 == g.field5 && g.field5 == "O" && g.field7 == "") button = "7";

                // Chech diagnally from bottom to top, both X and O 
                if (g.field[2, 0] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[0, 2] == "") button = "3";
                if (g.field[2, 2] == g.field[1, 1] && g.field[1, 1] == "X" && g.field[0, 0] == "") button = "1";

                // if (g.field7 == g.field5 && g.field5 == "O" && g.field3 == "") button = "3";
                // if (g.field9 == g.field5 && g.field5 == "O" && g.field1 == "") button = "1";
            }

            // returns button value if not ""
            if (button != "")
            {
                return button;
            }

            return "";
        }
        
        private static void OldCode()
        {
            /* WInner
           if (Result != "")
           {
               if (Result == "X")
               {
                   theWinner = new string[] { textPlayerOneName.Text, (Int32.Parse(labelPlayerOneScoreShow.Text) + 1).ToString() };
               }
               else
               {
                   theWinner = new string[] { textPlayerOneName.Text, (Int32.Parse(labelPlayerOneScoreShow.Text) + 1).ToString() };
               }



               if (Winner == "X")
               {
                   if (textPlayerOneName.Text != "")
                   {
                       MessageBox.Show("The winner is " + textPlayerOneName.Text, "Winner found!");
                   }
                   else
                       MessageBox.Show("The winner is " + Winner, "Winner found!");

                   int score = Int32.Parse(labelPlayerOneScoreShow.Text) + 1;
                   labelPlayerOneScoreShow.Text = score.ToString();
               }
               if (Winner == "O")
               {
                   if (textPlayerOneName.Text != "")
                   {
                       MessageBox.Show("The winner is " + textPlayerTwoName.Text, "Winner found!");
                   }
                   else
                       MessageBox.Show("The winner is " + Winner, "Winner found!");

                   int score = Int32.Parse(labelPlayerTwoScoreShow.Text) + 1;
                   labelPlayerTwoScoreShow.Text = score.ToString();
               }

               theWinner = null;
               ResetGame();
           }




            // playfield

            String[] Text = new string[9] { this.Controls[12].Text, this.Controls[13].Text, this.Controls[14].Text, this.Controls[15].Text, this.Controls[16].Text,
            this.Controls[17].Text, this.Controls[18].Text, this.Controls[19].Text, this.Controls[20].Text};

            playBoard[0, 0] = field.field[0, 0];
            playBoard[0, 1] = field.field[0, 1];
            playBoard[0, 2] = field.field[0, 2];
            playBoard[1, 0] = field.field[1, 0];
            playBoard[1, 1] = field.field[1, 1];
            playBoard[1, 2] = field.field[1, 2];
            playBoard[2, 0] = field.field[2, 0];
            playBoard[2, 1] = field.field[2, 1];
            playBoard[2, 2] = field.field[2, 2];
            
            // string[,] playBoard = new string[3, 3];
              public string field1;
                    public string field2;
                    public string field3;
                    public string field4;
                    public string field5;
                    public string field6;
                    public string field7;
                    public string field8;
                    public string field9;
                    
                        // Creates a string array where the texts from the button will be placed
            string[,] Text = new string[3,3];

            // Places the text from the buttons into this array
            Text[0, 0] = con[20].Text;
            Text[0, 1] = con[19].Text;
            Text[0, 2] = con[18].Text;
            Text[1, 0] = con[17].Text;
            Text[1, 1] = con[16].Text;
            Text[1, 2] = con[15].Text;
            Text[2, 0] = con[14].Text;
            Text[2, 1] = con[13].Text;
            Text[2, 2] = con[12].Text;


            //public char[] field = new char[9];

            public PlayField(Control contrls)
            {
                string[] S = {"aaaaa",
                        "bbbbb",
                        "ccccc"};
                char[] C = string.Join(string.Empty, S).ToCharArray();
                char[] charArray = Text.SelectMany(x => x.ToCharArray()).ToArray();
             

            //field[0] = Text[8];
            //field[1] = Text[7];
            //field[2] = Text[6];

            // second row
            field[1][0] = Text[5];
            field[1][1] = Text[4];
            field[1][2] = Text[3];

            // third row
            field[2][0] = Text[2];
            field[2][1] = Text[2];
            field[2][2] = Text[0];
           
            field1 = Text[8];
            field2 = Text[7];
            field3 = Text[6];

            field4 = Text[5];
            field5 = Text[4];
            field6 = Text[3];

            field7 = Text[2];
            field8 = Text[1];
            field9 = Text[0];
            }

          field[0][1] = Text[8];
          field[0][2] = Text[7];
          field[0][3] = Text[6];
          field[1][1] = Text[5];
          field[1][2] = Text[4];
          field[1][3] = Text[3];
          field[2][1] = Text[2];
          field[2][2] = Text[1];


          field[0] = controls[20].Text;
          field[1] = controls[19].Text;
          field[2] = controls[18].Text;

          field[3] = controls[17].Text;
          field[4] = controls[16].Text;
          field[5] = controls[15].Text;

          field[6] = controls[14].Text;
          field[7] = controls[13].Text;
          field[8] = controls[12].Text;

          foreach (Control c in controls)
          {
              if (c is Button && c.Text != "1" && c.Text != "2")
              {
                  char[] chr = c.Text.ToCharArray();
              }
          }
          */

        }
    }
}
