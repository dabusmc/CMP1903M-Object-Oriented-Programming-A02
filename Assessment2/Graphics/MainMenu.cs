using System;
using System.Collections.Generic;

namespace Assessment2
{
    class MainMenu : IDrawable
    {
        private readonly List<Game> m_Games = new List<Game>()
        {
            new ThreeOrMore(),
            new SevensOut(),
            new Statistics(),
            new Testing()
        };

        private int m_Selection = 0;
        private bool m_InGame = false;
        private bool m_MoveBack = false;
        private Game m_SelectedProgram = null;

        public List<string> Draw(ConsoleKeyInfo keyInput)
        {
            List<string> toDraw = new List<string>();

            if(m_InGame == false)
            {
                toDraw = new List<string>
                {
                    "=== Main Menu ===",
                    "================="
                };

                if(keyInput.Key == ConsoleKey.DownArrow)
                {
                    m_Selection += 1;
                }
                else if(keyInput.Key == ConsoleKey.UpArrow)
                {
                    m_Selection -= 1;
                }
                else if(keyInput.Key == ConsoleKey.Enter)
                {
                    // Run the selected game
                    m_SelectedProgram = m_Games[m_Selection];
                    m_InGame = true;

                    // Generate the next frame then return so the menu isn't rendered
                    toDraw = Draw(keyInput);
                    return toDraw;
                }

                if (m_Selection >= m_Games.Count)
                {
                    m_Selection = m_Games.Count - 1;
                }

                if (m_Selection < 0)
                {
                    m_Selection = 0;
                }

                for (int i = 0; i < m_Games.Count; i++)
                {
                    Game game = m_Games[i];

                    if(i == m_Selection)
                    {
                        toDraw.Add("> " + game.Name);
                    }
                    else
                    {
                        toDraw.Add("  " + game.Name);
                    }
                }
            }
            else
            {
                if(m_SelectedProgram.IsPlaying())
                {
                    toDraw = m_SelectedProgram.Draw(keyInput);
                    if(!m_SelectedProgram.IsPlaying())
                    {
                        return Draw(keyInput);
                    }
                }
                else
                {
                    if (m_Selection < 2)
                    {
                        if (m_Selection == 0)
                        {
                            toDraw.Add("=== Sevens Out ===");
                            toDraw.Add("==================");
                        }
                        else if (m_Selection == 1)
                        {
                            toDraw.Add("=== Three or More ===");
                            toDraw.Add("=====================");
                        }

                        int winner = m_SelectedProgram.GetWinner();
                        toDraw.Add("Game Over!");

                        // Three or More
                        if (m_Selection == 0)
                        {
                            toDraw.Add("Player " + (winner + 1) + " wins!");
                            if (winner == 0) Statistics.Instance.PlayerOneWins += 1;
                            else Statistics.Instance.PlayerTwoWins += 1;

                            int[] scores = m_SelectedProgram.GetPlayerScores();

                            if(Statistics.Instance.TOMHighScore < scores[0])
                            {
                                Statistics.Instance.TOMHighScore = scores[0];
                                Statistics.Instance.TOMHighScorePlayer = 0;
                            }

                            if(Statistics.Instance.TOMHighScore < scores[1])
                            {
                                Statistics.Instance.TOMHighScore = scores[1];
                                Statistics.Instance.TOMHighScorePlayer = 1;
                            }
                        }
                        // Sevens Out
                        else if (m_Selection == 1)
                        {
                            if (winner == -1)
                            {
                                toDraw.Add("It was a tie! Both players had " + m_SelectedProgram.GetPlayerScores()[0] + " points");
                            }
                            else if (winner == 0)
                            {
                                toDraw.Add("Player 1 wins! " + m_SelectedProgram.GetPlayerScores()[0] + " points > " + m_SelectedProgram.GetPlayerScores()[1] + " points");
                                Statistics.Instance.PlayerOneWins += 1;
                            }
                            else if (winner == 1)
                            {
                                toDraw.Add("Player 2 wins! " + m_SelectedProgram.GetPlayerScores()[1] + " points > " + m_SelectedProgram.GetPlayerScores()[0] + " points");
                                Statistics.Instance.PlayerTwoWins += 1;
                            }
                        }

                        toDraw.Add("Press Enter to return to Main Menu");

                        if (!m_MoveBack)
                        {
                            m_MoveBack = true;
                            return toDraw;
                        }

                        if (keyInput.Key == ConsoleKey.Enter)
                        {
                            m_SelectedProgram.Reset();
                            m_InGame = false;
                            Statistics.Instance.NumberOfGamesPlayed += 1;
                            ConsoleKeyInfo info = new ConsoleKeyInfo();
                            toDraw = Draw(info);
                        }
                    }
                    else
                    {
                        m_SelectedProgram.Reset();
                        m_InGame = false;
                        ConsoleKeyInfo info = new ConsoleKeyInfo();
                        toDraw = Draw(info);
                    }
                }
            }

            return toDraw;
        }
    }
}
