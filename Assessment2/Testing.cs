using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Assessment2
{
    class Testing : Game
    {
        private bool m_TestsRun = false;
        private bool m_LeaveProgram = false;

        public Testing() : base("Testing", 0)
        {
        }

        public override List<string> Draw(ConsoleKeyInfo keyInput)
        {
            List<string> draw = new List<string>();

            draw.Add("=== Testing ===");
            draw.Add("===============");

            // Prompt user to Run Tests
            draw.Add("Press Enter to Run Tests");

            if(!m_TestsRun)
            {
                m_TestsRun = true;
                return draw;
            }

            draw.Add("");

            ConsoleKeyInfo enterInfo = new ConsoleKeyInfo('a', ConsoleKey.Enter, false, false, false);
            ConsoleKeyInfo aInfo = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);

            // Test ThreeOrMore
            ThreeOrMore tom = new ThreeOrMore();
            draw.Add("Three Or More");

            // Test for scores being set
            int[] prevScores = new int[2];
            tom.Draw(enterInfo);
            int[] scores = tom.GetPlayerScores();
            Debug.Assert(scores.SequenceEqual(prevScores));

            // Display test result
            draw.Add("  Scores are zero at beginning: passed");

            // Test for scores being set after round
            tom.Draw(enterInfo);
            tom.Draw(aInfo);
            scores = tom.GetPlayerScores();

            if(tom.GetCurrentPlayer() == 1)
            {
                tom.Draw(enterInfo);
            }

            Debug.Assert(!scores.SequenceEqual(prevScores));

            // Display test result
            draw.Add("  Scores are assigned properly after a round: passed");

            draw.Add("");

            // Prompt user to exit testing
            draw.Add("Press Enter to Exit Testing");

            if(!m_LeaveProgram)
            {
                m_LeaveProgram = true;
            }
            else
            {
                GameEnded = true;
            }

            return draw;
        }

        public override int GetWinner()
        {
            return 0;
        }

        public override void Reset()
        {
            GameEnded = false;
            m_LeaveProgram = false;
            m_TestsRun = false;
        }
    }
}
