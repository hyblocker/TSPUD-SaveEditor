using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace StanleySaveEditor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool launchNormally = true;
            const string tspudTitle = "The Stanley Parable: Ultra Deluxe Save Editor";
            string executableTitle = Process.GetCurrentProcess().MainModule.ModuleName;
            executableTitle = executableTitle.Substring(0, executableTitle.IndexOf('.'));

            for (int i = 0; i < args.Length; i++)
            {
                var current = args[i];

                switch(current)
                {
                    case "--import":
                        launchNormally = false;
                        SaveHandler.Init();
                        SaveHandler.Load(string.Join(" ", args).Substring(args[0].Length + 1));
                        Console.WriteLine("Successfully imported save!");
                        break;
                    case "--export":
                        launchNormally = false;
                        SaveHandler.Init();
                        SaveHandler.Save(string.Join(" ", args).Substring(args[0].Length + 1));
                        Console.WriteLine("Successfully exported save!");
                        break;
                    case "-h":
                    case "--help":
                        launchNormally = false;
                        Console.WriteLine($@"NAME:
{'\t'}{executableTitle} - A save editor for {tspudTitle}

USAGE:
{'\t'}{executableTitle} [command]

VERSION:
{'\t'}{Application.ProductVersion}

COMMANDS:
{'\t'}--help, -h{'\t'}show help (default: false)
{'\t'}--export path{'\t'}exports the save as a JSON object to the specified path (default: false)
{'\t'}--import path{'\t'}imports the save from the specified path, and overwrites the current save (default: false)
{'\t'}--version, v{'\t'}print the version (default: false)");
                        break;
                    case "-v":
                    case "--version":
                        launchNormally = false;
                        Console.WriteLine($@"{executableTitle} version {Application.ProductVersion}");
                        break;
                }
            }

            if (!launchNormally) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
