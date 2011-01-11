using System;
using System.Threading;

namespace Sept1983Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Thread serverThread;
            //serverThread = new Thread(new ThreadStart(Program.showDummyGUI));
            //serverThread.Start();
            ProgramServer.Run();
        }

        public static void showDummyGUI() {
            Gui gui = new Gui();
            gui.Show();
        }
    }
}

