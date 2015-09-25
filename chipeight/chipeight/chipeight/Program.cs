using System;
using eightmulator;

namespace chipeight
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Emulator emul8 = new Emulator();
            MainForm form = new MainForm(emul8);
            form.Show();
            Main game = new Main(form,emul8);
            game.Run();  
        }
    }
#endif
}

