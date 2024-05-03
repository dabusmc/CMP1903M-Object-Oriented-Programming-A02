using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    class Graphics
    {
        public static Graphics Instance { get; private set; }

        public bool Initialised { get; private set; }

        private bool m_Running;

        private List<IDrawable> m_DrawQueue = new List<IDrawable>();

        public Graphics()
        {
            if(Instance != null)
            {
                Console.WriteLine("There should only ever be one instance of Graphics");
                Initialised = false;
                m_Running = false;
            }
            else
            {
                Instance = this;
                m_Running = true;
                Initialise();
            }
        }

        public void Initialise()
        {
            Initialised = true;
            Clear();
            m_DrawQueue?.Clear();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Draw(IDrawable toDraw)
        {
            m_DrawQueue?.Add(toDraw);
        }

        public void ClearDrawQueue()
        {
            m_DrawQueue?.Clear();
        }

        public void Loop()
        {
            ConsoleKeyInfo currentFrameKeyInfo = default(ConsoleKeyInfo);

            int frames = 0;
            while(true)
            {
                if (currentFrameKeyInfo.Key == ConsoleKey.Escape)
                {
                    Stop();
                    break;
                }

                Clear();
                foreach(IDrawable drawable in m_DrawQueue)
                {
                    List<string> toDraw = drawable.Draw(currentFrameKeyInfo);
                    foreach(string draw in toDraw)
                    {
                        Console.WriteLine(draw);
                    }
                }

                // Do this to make sure we draw the first frame before asking for input
                if (frames > 0)
                {
                    currentFrameKeyInfo = WaitForInput();
                }

                frames += 1;
            }
        }

        public ConsoleKeyInfo WaitForInput()
        {
            return Console.ReadKey();
        }

        public void Stop()
        {
            m_Running = false;
        }
    }
}
