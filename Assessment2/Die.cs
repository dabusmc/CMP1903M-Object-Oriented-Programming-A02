using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    // This class represents a Die using random number generation
    class Die
    {
        private static Random s_RandomEngine = new Random();
       
        public int CurrentValue { get; private set; }

        // Simulates a die roll and generates the value from between 1 and 6
        public int Roll()
        {
            // Max value is 7 here in order to include 6 as a possible output as
            // Random.Next(min, max) generates a number where min <= number < max
            CurrentValue = s_RandomEngine.Next(1, 7);
            return CurrentValue;
        }
    }
}
