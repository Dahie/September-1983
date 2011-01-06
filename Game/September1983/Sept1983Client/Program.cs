using System;

namespace Sept1983Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameClient game = new GameClient())
            {
                game.Run();
            }
        }
    }
}

