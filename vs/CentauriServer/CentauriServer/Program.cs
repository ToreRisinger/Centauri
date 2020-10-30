using System;
using System.Threading;

namespace Server
{
    class Program
    {

        private static bool isRunning = false;
        static void Main(string[] args)
        {
            try
            {
                
                Console.Title = "Game Server";
                isRunning = true;

                Thread mainThread = new Thread(new ThreadStart(MainThread));
                mainThread.Start();

                GameServer.Start(10, 26950);
            }
            catch (Exception e) {
                Console.Write(e.StackTrace.ToString());
            }
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            GameLogic.init();

            while(isRunning)
            {
                while(_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if(_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine((e.ExceptionObject as Exception).Message);
        }
    }
}
