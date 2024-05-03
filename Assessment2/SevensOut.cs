using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    class SevensOut : Game
    {
        private const int m_DieCount = 2;

        private bool m_HasFirstRoll = false;
        private bool m_HasSecondRoll = false;

        public SevensOut() : base("Sevens Out", m_DieCount)
        {
        }

        public override int GetWinner()
        {
            if (PlayerScores[0] == PlayerScores[1])
            {
                return -1;
            }

            if (PlayerScores[0] > PlayerScores[1])
            {
                return 0;
            }

            return 1;
        }

        public override List<string> Draw(ConsoleKeyInfo keyInput)
        {
            List<string> generatedDisplay = new List<string>();

            // Display Game's title
            generatedDisplay.Add("=== Sevens Out ===");
            generatedDisplay.Add("==================");

            // Allow user to roll
            generatedDisplay.Add("Player " + (CurrentPlayer + 1) + ", Press Enter to Roll First Die");

            if (CurrentPlayer == 0 || (CurrentPlayer == 1 && Opponent == Opponent.PLAYER))
            {
                // Get player to press enter
                if(Iterations == 0)
                {
                    Iterations += 1;
                    return generatedDisplay;
                }

                if(keyInput.Key == ConsoleKey.Enter)
                {
                    // Roll the dice
                    if(!m_HasFirstRoll)
                    {
                        int firstValue = Dice[0].Roll();
                        generatedDisplay.Add("Your first roll was a " + firstValue);
                        generatedDisplay.Add("Press Enter to Roll Second Die");
                        m_HasFirstRoll = true;
                        Iterations += 1;
                        return generatedDisplay;
                    }
                    else
                    {
                        generatedDisplay.Add("Your first roll was a " + Dice[0].CurrentValue);
                        generatedDisplay.Add("Press Enter to Roll Second Die");
                        int secondValue = 0;

                        if(!m_HasSecondRoll)
                        {
                            secondValue = Dice[1].Roll();
                        }
                        else
                        {
                            secondValue = Dice[1].CurrentValue;
                        }

                        generatedDisplay.Add("Your second roll was a " + secondValue);
                        m_HasSecondRoll = true;
                    }

                    // Calculate the two values' total
                    int total = Dice[0].CurrentValue + Dice[1].CurrentValue;

                    // Display total to player
                    generatedDisplay.Add("Your total for these rolls, was " + total);

                    // If the total is 7, the game should end
                    if(total == 7)
                    {
                        GameEnded = true;
                        Iterations += 1;
                        return generatedDisplay;
                    }

                    // If the total isn't seven, calculate the amount of points this player gets
                    // If the two rolls are equal, the player should get double points
                    if (Dice[0].CurrentValue == Dice[1].CurrentValue)
                    {
                        PlayerScores[CurrentPlayer] += total * 2;

                        // Tell user how many points are being added
                        generatedDisplay.Add("Player " + (CurrentPlayer + 1) + " gets " + (total * 2) + " points!");
                    }
                    else
                    {
                        // Lastly, if the two rolls aren't equal and the total isn't 7, then the total is added to the players score
                        PlayerScores[CurrentPlayer] += total;

                        // Tell user how many points are being added
                        generatedDisplay.Add("Player " + (CurrentPlayer + 1) + " gets " + total + " points!");
                    }

                    // Display the current players score
                    generatedDisplay.Add("Player " + (CurrentPlayer + 1) + "'s Score: " + PlayerScores[CurrentPlayer]);

                    // Prompt to move onto next player
                    generatedDisplay.Add("Press Enter to move onto Player " + (GetNextPlayer() + 1) + "'s Turn");

                    // Reset relevant variables for next turn
                    m_HasFirstRoll = false;
                    m_HasSecondRoll = false;

                    // Swap to next player
                    NextPlayersTurn();
                }
                else
                {
                    return generatedDisplay;
                }
            }
            else
            {
                // We are versing a computer and it is their turn
                // Roll the dice
                int firstValue = Dice[0].Roll();
                int secondValue = Dice[1].Roll();

                generatedDisplay.Add("Your first roll was a " + firstValue);
                generatedDisplay.Add("Your second roll was a " + secondValue);

                // Calculate the two values' total
                int total = Dice[0].CurrentValue + Dice[1].CurrentValue;

                // Display total to player
                generatedDisplay.Add("Your total for these rolls, was " + total);

                // If the total is 7, the game should end
                if (total == 7)
                {
                    generatedDisplay.Add("Looks like you found the seven. Game Over!");
                    GameEnded = true;
                    Iterations += 1;
                    return generatedDisplay;
                }

                // If the total isn't seven, calculate the amount of points this player gets
                // If the two rolls are equal, the player should get double points
                if (Dice[0].CurrentValue == Dice[1].CurrentValue)
                {
                    PlayerScores[CurrentPlayer] += total * 2;

                    // Tell user how many points are being added
                    generatedDisplay.Add("Player " + (CurrentPlayer + 1) + " gets " + (total * 2) + " points!");
                }
                else
                {
                    // Lastly, if the two rolls aren't equal and the total isn't 7, then the total is added to the players score
                    PlayerScores[CurrentPlayer] += total;

                    // Tell user how many points are being added
                    generatedDisplay.Add("Player " + (CurrentPlayer + 1) + " gets " + total + " points!");
                }

                // Display the current players score
                generatedDisplay.Add("Player " + (CurrentPlayer + 1) + "'s Score: " + PlayerScores[CurrentPlayer]);

                // Prompt to move onto next player
                generatedDisplay.Add("Press Enter to move onto Player " + (GetNextPlayer() + 1) + "'s Turn");

                // Reset relevant variables for next turn
                m_HasFirstRoll = false;
                m_HasSecondRoll = false;

                // Swap to next player
                NextPlayersTurn();
            }

            Iterations += 1;
            return generatedDisplay;
        }

        public override void Reset()
        {
            CurrentPlayer = 0;
            GameEnded = false;
            PlayerScores = new int[2];

            Dice = new Die[m_DieCount];
            for (int i = 0; i < m_DieCount; i++)
            {
                Dice[i] = new Die();
            }
        }
    }
}
