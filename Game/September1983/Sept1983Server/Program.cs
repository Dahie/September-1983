using System;

namespace Sept1983Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameServer game = new GameServer())
            {
                game.Run();
            }
        }
    }
}

