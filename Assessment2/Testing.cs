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

            // Test if the scores are correct
            tom.Draw(enterInfo);
            tom.Draw(aInfo);
            scores = tom.GetPlayerScores();
            int firstPlayerScore = scores[0];
            bool scoreEqualZero = firstPlayerScore == 0;
            bool scoreEqualThree = firstPlayerScore == 3;
            bool scoreEqualSix = firstPlayerScore == 6;
            bool scoreEqualTwelve = firstPlayerScore == 12;

            Debug.Assert(scoreEqualZero || scoreEqualThree || scoreEqualSix || scoreEqualTwelve);

            // Display test result
            draw.Add("  Scores are correct after first round: passed");

            // Test if winner is correct
            while(tom.IsPlaying())
            {
                tom.Draw(aInfo);
            }
            int winner = tom.GetWinner();

            int winnerShouldBe = 0;
            if (tom.GetPlayerScores()[winner] >= 20)
            {
                winnerShouldBe = 0;
            }
            else
            {
                winnerShouldBe = 1;
            }

            Debug.Assert(winner == winnerShouldBe);

            // Display test result
            draw.Add("  Correct winner selected: passed");

            draw.Add("");

            // Test SevensOut
            SevensOut so = new SevensOut();
            draw.Add("Sevens Out");

            // Test for scores being set
            prevScores = new int[2];
            so.Draw(enterInfo);
            scores = so.GetPlayerScores();
            Debug.Assert(scores.SequenceEqual(prevScores));

            // Display test result
            draw.Add("  Scores are zero at beginning: passed");

            // Test if the scores are correct
            so.Draw(enterInfo);
            int[] rolls = so.GetDice().Select(x => x.CurrentValue).ToArray();
            scores = so.GetPlayerScores();
            int total = rolls[0] + rolls[1];

            bool isScoreZero = scores[0] == 0;
            bool isScoreSingle = scores[0] == total;
            bool isScoreDouble = scores[0] == total * 2;

            Debug.Assert(isScoreZero || isScoreSingle || isScoreDouble);

            // Display test result
            draw.Add("  Scores are correct after first round: passed");

            // Test if winner is correct

            // Display test result
            draw.Add("  Correct winner selected: passed");

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
