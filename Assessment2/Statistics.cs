using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Assessment2
{
    class Statistics : Game
    {
        public static Statistics Instance = new Statistics();

        private bool m_LeaveProgram = false;

        public int NumberOfGamesPlayed = 0;
        public int PlayerOneWins = 0;
        public int PlayerTwoWins = 0;
        public int TOMHighScore = -1;
        public int TOMHighScorePlayer = -1;
        public int SOHighScore = -1;
        public int SOHighScorePlayer = -1;

        public Statistics() : base("Statistics", 0)
        {
            Instance = this;
        }

        public override List<string> Draw(ConsoleKeyInfo keyInput)
        {
            List<string> display = new List<string>();

            display.Add("=== Statistics ===");
            display.Add("==================");

            // Display statistics
            display.Add("Number of Games Played: " + NumberOfGamesPlayed);
            display.Add("Player One Wins: " + PlayerOneWins);
            display.Add("Player Two Wins: " + PlayerTwoWins);

            if(TOMHighScore == -1)
            {
                display.Add("Three Or More High Score: None");
            }
            else
            {
                display.Add("Three Or More High Score: " + TOMHighScore + " (Player " + (TOMHighScorePlayer + 1) + ")");
            }

            if(SOHighScore == -1)
            {
                display.Add("Sevens Out High Score: None");
            }
            else
            {
                display.Add("Sevens Out High Score: " + SOHighScore + " (Player " + (SOHighScorePlayer + 1) + ")");
            }

            // Do this so that the user exits when pressing enter
            display.Add("Press Enter to exit");

            if(!m_LeaveProgram)
            {
                m_LeaveProgram = true;
            }
            else
            {
                GameEnded = true;
            }

            return display;
        }

        public override int GetWinner()
        {
            return -1;
        }

        public override void Reset()
        {
            m_LeaveProgram = false;
            GameEnded = false;
        }
    }
}
