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
        }
    }
}
