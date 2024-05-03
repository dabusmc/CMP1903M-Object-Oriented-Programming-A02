using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    public enum Opponent
    {
        COMPUTER,
        PLAYER
    }

    abstract class Game
    {
        public string Name { get; protected set; }
        public Opponent Opponent { get; protected set; }
        protected Die[] Dice { get; set; }
        protected bool GameEnded { get; set; }
        protected int CurrentPlayer { get; set; }
        protected int Iterations { get; set; }
        protected int[] PlayerScores { get; set; }

        public Game(string name, int dieCount)
        {
            Name = name;
            GameEnded = false;

            PlayerScores = new int[2];

            Dice = new Die[dieCount];
            for(int i = 0; i < dieCount; i++)
            {
                Dice[i] = new Die();
            }
        }

        // Get all of the player scores
        public int[] GetPlayerScores()
        {
            return PlayerScores;
        }

        // Get the current player
        public int GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        // Set the opponent type for the game
        public void SetOpponent(Opponent opponent)
        {
            Opponent = opponent;
        }

        // This function represents whether or not the game should continue into the next loop
        public bool IsPlaying() { return !GameEnded; }

        // This function swaps the current player
        public void NextPlayersTurn()
        {
            CurrentPlayer = GetNextPlayer();
        }

        // This function gets the index of the player who's turn is next
        public int GetNextPlayer()
        {
            if(CurrentPlayer == 0) { return 1; }
            return 0;
        }

        // This function returns the index of the player who won the game or -1 if it was a draw
        public abstract int GetWinner();

        // This function resets the game to its default parameters
        public abstract void Reset();

        // This function gets called once per frame and is essentially the update program of the Game
        public abstract List<string> Draw(ConsoleKeyInfo keyInput);
    }
}
