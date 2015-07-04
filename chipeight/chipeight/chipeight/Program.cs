using System;

namespace chipeight
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            MainForm form = new MainForm();
            form.Show();
            Game1 game = new Game1(form.getDrawSurface());
            game.Run();  
        }
    }
#endif
}

