using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    class ThreeOrMore : Game
    {
        private const int m_DieCount = 5;
        private int m_FirstToTwenty = 0;

        private bool m_RolledDice = false;
        private bool m_HasChosenReroll = false;
        private bool m_NextPlayersTurn = false;
        private bool m_HasRerolled = false;
        private bool m_RerollRemaining = false;

        public ThreeOrMore() : base("Three or More", m_DieCount)
        {
        }

        public override int GetWinner()
        {
            return m_FirstToTwenty;
        }

        public override List<string> Draw(ConsoleKeyInfo keyInput)
        {
            List<string> display = new List<string>();

            display.Add("=== Three or More ===");
            display.Add("=====================");

            if (CurrentPlayer == 0 || (CurrentPlayer == 1 && Opponent == Opponent.PLAYER))
            {
                display.Add("Player " + (CurrentPlayer + 1) + ", press enter to roll your dice");
                if (!m_RolledDice)
                {
                    for (int i = 0; i < m_DieCount; i++)
                    {
                        Dice[i].Roll();
                    }

                    m_RolledDice = true;
                    Iterations += 1;
                    return display;
                }

                // Roll all dice and gather the values
                int[] values = Dice.Select(x => x.CurrentValue).ToArray();
                m_RolledDice = true;

                // Display values rolled
                string valuesDisplay = "";
                for (int i = 0; i < values.Length; i++)
                {
                    valuesDisplay += values[i].ToString();
                    if (i < values.Length - 1)
                    {
                        valuesDisplay += ", ";
                    }
                }
                display.Add("You rolled: " + valuesDisplay);

                // Look for five-of-a-kind
                bool fiveOfAKind = false;
                if (values.All(v => v == values[0]))
                {
                    display.Add("That's 5-of-a-kind. 12 points to Player " + (CurrentPlayer + 1));
                    PlayerScores[CurrentPlayer] += 12;
                    fiveOfAKind = true;
                }

                if (!fiveOfAKind)
                {
                    // Look for four-of-a-kind
                    bool fourOfAKind = false;
                    foreach (int v in values)
                    {
                        int[] check = values.Where(x => x == v).ToArray();
                        if (check.Length == 4)
                        {
                            fourOfAKind = true;
                            break;
                        }
                    }

                    if (fourOfAKind)
                    {
                        display.Add("That's 4-of-a-kind. 6 points to Player " + (CurrentPlayer + 1));
                        PlayerScores[CurrentPlayer] += 6;
                    }
                    else
                    {
                        // Look for three-of-a-kind
                        bool threeOfAKind = false;
                        foreach (int v in values)
                        {
                            int[] check = values.Where(x => x == v).ToArray();
                            if (check.Length == 3)
                            {
                                threeOfAKind = true;
                                break;
                            }
                        }

                        if (threeOfAKind)
                        {
                            display.Add("That's 3-of-a-kind. 3 points to Player " + (CurrentPlayer + 1));
                            PlayerScores[CurrentPlayer] += 3;
                        }
                        else
                        {
                            // Look for two-of-a-kind
                            bool twoOfAKind = false;
                            int[] check = null;
                            foreach (int v in values)
                            {
                                check = values.Where(x => x == v).ToArray();
                                if (check.Length == 2)
                                {
                                    twoOfAKind = true;
                                    break;
                                }
                            }

                            if (twoOfAKind)
                            {
                                display.Add("That's 2-of-a-kind!");

                                // Player only gets one re-roll per 2-of-a-kind
                                if (!m_HasRerolled)
                                {
                                    // Prompt user to choose whether or not to reroll all dice or remaining dice
                                    display.Add("Press 'a' to reroll all dice, or 'r' to reroll remaining dice");

                                    if (!m_HasChosenReroll)
                                    {
                                        m_HasChosenReroll = true;
                                        Iterations += 1;
                                        return display;
                                    }
                                    else
                                    {
                                        // Check key input for 'a' or 'r'
                                        if (keyInput.Key == ConsoleKey.A)
                                        {
                                            m_RerollRemaining = false;
                                        }
                                        else if (keyInput.Key == ConsoleKey.R)
                                        {
                                            m_RerollRemaining = true;
                                        }
                                        else
                                        {
                                            m_HasChosenReroll = false;
                                            Iterations += 1;
                                            return display;
                                        }

                                        m_HasRerolled = true;

                                        // Reroll only remaining dice
                                        if (m_RerollRemaining)
                                        {
                                            for (int i = 0; i < m_DieCount; i++)
                                            {
                                                if (Dice[i].CurrentValue == check[0])
                                                {
                                                    continue;
                                                }

                                                // Make it impossible for the dice to have the same value
                                                int currentValue = Dice[i].CurrentValue;
                                                while (currentValue == Dice[i].CurrentValue)
                                                {
                                                    Dice[i].Roll();
                                                }
                                            }
                                            m_HasChosenReroll = false;
                                            display = Draw(keyInput);
                                            Iterations += 1;
                                            return display;
                                        }
                                        // Reroll all dice
                                        else
                                        {
                                            for (int i = 0; i < m_DieCount; i++)
                                            {
                                                // Make it impossible for the dice to have the same value
                                                int currentValue = Dice[i].CurrentValue;
                                                while (currentValue == Dice[i].CurrentValue)
                                                {
                                                    Dice[i].Roll();
                                                }
                                            }

                                            m_HasChosenReroll = false;
                                            display = Draw(keyInput);
                                            Iterations += 1;
                                            return display;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // We are playing against the computer, and its their turn
                display.Add("Player " + (CurrentPlayer + 1) + ", press enter to roll your dice");

                // Roll dice
                for (int i = 0; i < m_DieCount; i++)
                {
                    Dice[i].Roll();
                }

                // Roll all dice and gather the values
                int[] values = Dice.Select(x => x.CurrentValue).ToArray();
                m_RolledDice = true;

                // Display values rolled
                string valuesDisplay = "";
                for (int i = 0; i < values.Length; i++)
                {
                    valuesDisplay += values[i].ToString();
                    if (i < values.Length - 1)
                    {
                        valuesDisplay += ", ";
                    }
                }
                display.Add("You rolled: " + valuesDisplay);

                // Look for five-of-a-kind
                bool fiveOfAKind = false;
                if (values.All(v => v == values[0]))
                {
                    display.Add("That's 5-of-a-kind. 12 points to Player " + (CurrentPlayer + 1));
                    PlayerScores[CurrentPlayer] += 12;
                    fiveOfAKind = true;
                }

                if (!fiveOfAKind)
                {
                    // Look for four-of-a-kind
                    bool fourOfAKind = false;
                    foreach (int v in values)
                    {
                        int[] check = values.Where(x => x == v).ToArray();
                        if (check.Length == 4)
                        {
                            fourOfAKind = true;
                            break;
                        }
                    }

                    if (fourOfAKind)
                    {
                        display.Add("That's 4-of-a-kind. 6 points to Player " + (CurrentPlayer + 1));
                        PlayerScores[CurrentPlayer] += 6;
                    }
                    else
                    {
                        // Look for three-of-a-kind
                        bool threeOfAKind = false;
                        foreach (int v in values)
                        {
                            int[] check = values.Where(x => x == v).ToArray();
                            if (check.Length == 3)
                            {
                                threeOfAKind = true;
                                break;
                            }
                        }

                        if (threeOfAKind)
                        {
                            display.Add("That's 3-of-a-kind. 3 points to Player " + (CurrentPlayer + 1));
                            PlayerScores[CurrentPlayer] += 3;
                        }
                        else
                        {
                            // Look for two-of-a-kind
                            bool twoOfAKind = false;
                            int[] check = null;
                            foreach (int v in values)
                            {
                                check = values.Where(x => x == v).ToArray();
                                if (check.Length == 2)
                                {
                                    twoOfAKind = true;
                                    break;
                                }
                            }

                            if (twoOfAKind)
                            {
                                display.Add("That's 2-of-a-kind!");
                                display.Add("Press 'a' to reroll all dice, or 'r' to reroll remaining dice");
                            }
                        }
                    }
                }
            }

            // Print scores
            display.Add("");
            display.Add("Scores:");
            display.Add("   Player 1: " + PlayerScores[0]);
            display.Add("   Player 2: " + PlayerScores[1]);

            // Check if anyone has won
            if (PlayerScores[0] >= 20)
            {
                m_FirstToTwenty = 0;
                Iterations += 1;
                GameEnded = true;
                return display;
            }
            else if (PlayerScores[1] >= 20)
            {
                m_FirstToTwenty = 1;
                Iterations += 1;
                GameEnded = true;
                return display;
            }

            // Prompt user to move onto next turn
            display.Add("");
            display.Add("Press Enter to move onto Player " + (GetNextPlayer() + 1) + "'s turn");

            // Move onto next player
            m_HasChosenReroll = false;
            m_HasRerolled = false;
            m_RolledDice = false;
            m_NextPlayersTurn = false;
            NextPlayersTurn();

            Iterations += 1;
            return display;
        }

        public override void Reset()
        {
            CurrentPlayer = 0;
            GameEnded = false;
            PlayerScores = new int[2];
            m_RolledDice = false;
            m_HasChosenReroll = false;
            m_NextPlayersTurn = false;

            Dice = new Die[m_DieCount];
            for (int i = 0; i < m_DieCount; i++)
            {
                Dice[i] = new Die();
            }
        }
    }
}
