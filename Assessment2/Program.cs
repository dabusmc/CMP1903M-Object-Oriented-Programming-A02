using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Graphics();
            if(Graphics.Instance.Initialised)
            {
                Graphics.Instance.Draw(new MainMenu());
                Graphics.Instance.Loop();
            }

            //Game game = new ThreeOrMore(Opponent.COMPUTER);
            //while(game.IsPlaying())
            //{
            //    game.Loop();
            //}
            //Console.WriteLine();

            //int winner = game.GetWinner();
            //if (winner == -1)
            //{
            //    Console.WriteLine("It was a draw!");
            //}
            //else
            //{
            //    Console.WriteLine("Player " + (winner + 1) + " wins!");
            //}
        }
    }
}
