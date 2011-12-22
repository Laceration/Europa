using System;

namespace Europa
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (EuropaGame game = new EuropaGame())
            {
                game.Run();
            }
        }
    }
#endif
}

