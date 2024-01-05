using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvToClass.Utility;
using System.IO;

namespace CsvToClass
{
        class Program
    {
        static void Main(string[] args)
        {
            var commandLine = new Arguments(args);
            GetHelp(commandLine);
            var defaultInputFile = GetDefaultInputFile(commandLine);
            var destinationFilePath = GetDestinationFilePath(commandLine);
            var nameSpace = GetNamespace(commandLine);
            var runmode = GetRunmode(commandLine);

            DisplayInteractiveInstructions(runmode);

            GetOptionsInteractive(runmode, ref defaultInputFile, ref destinationFilePath);
            if (runmode == Enums.Runmode.Automatic)
            {
                Console.WriteLine("1. Source file: \n" + defaultInputFile);
                Console.WriteLine("2. Destination file: \n" + destinationFilePath);
                Console.WriteLine("3. namespace: \n" + nameSpace);
            }
            Console.WriteLine();

            Validate(defaultInputFile, destinationFilePath);

            Console.WriteLine("Processing File ...");
            var generator = new CsvClassGenerator.ClassGenerator();
            generator.GenerateClass(defaultInputFile, destinationFilePath, nameSpace.ToString());

            Console.WriteLine("Class written. Press any key.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void Validate(string defaultInputFile, string destinationFilePath)
        {
            if (!File.Exists(defaultInputFile))
            {
                Error("Error: Input file does not exist.");
            }
            //if (File.Exists(destinationFilePath))
            //{

            //    File.Move(destinationFilePath, destinationFilePath.Replace(".cs", ".bak"));
            //    Console.WriteLine("{0} has been copied to {1}.", destinationFilePath, destinationFilePath.Replace(".cs", ".bak"));
            //}
        }

        private static void GetOptionsInteractive(Enums.Runmode runmode, ref string defaultInputFile, ref string destinationFilePath)
        {
            if (runmode == Enums.Runmode.Interactive)
            {
                Console.WriteLine("1. Source file for ticker symbol and exchange list: \n" + defaultInputFile);
                defaultInputFile = Console.ReadLine();
                if (defaultInputFile != null && defaultInputFile.Length == 0)
                    defaultInputFile = Config.GetDefaultInputFile();
                Console.WriteLine("2. Destination Output root directory: \n" + destinationFilePath);
                destinationFilePath = Console.ReadLine();
                if (destinationFilePath != null && destinationFilePath.Length == 0)
                    destinationFilePath = Config.GetDefaultOutputFile();
            }
        }

        private static void DisplayInteractiveInstructions(Enums.Runmode runmode)
        {
            if (runmode == Enums.Runmode.Interactive)
            {
                //Document the process:
                Console.WriteLine("GenerateClassFromCsv ");
                Console.WriteLine("==============================================");
                Console.WriteLine("This progam reads a csv file with a header and ");
                Console.WriteLine(" by reading the data contained therein, infers the");
                Console.WriteLine(" data types and generates a C# class (.cs) file.");
                Console.WriteLine("Parameters are optional: ");
                Console.WriteLine("   1> Source File is a .csv data file.");
                Console.WriteLine("   2> Output File the class definition output is placed.");
                Console.WriteLine("   3> The namespace for the class.");
                Console.WriteLine();
                Console.WriteLine("NOTE: THIS PROGRAM WILL OVERWRITE AN EXISTING FILE.");
                Console.WriteLine("Press any key to Continue or Escape to quit.");
                Console.WriteLine();
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
        private static Enums.Runmode GetRunmode(Arguments commandLine)
        {
            var runmode = Enums.Runmode.Interactive; // default to interactive
            if (commandLine["a"] != null || commandLine["auto"] != null)
            {
                runmode = Enums.Runmode.Automatic;
            }
            if (commandLine["i"] != null || commandLine["interactive"] != null)
            {
                runmode = Enums.Runmode.Interactive;
            }
            return runmode;
        }

        private static object GetNamespace(Arguments commandLine)
        {
            var destinationDirectory = Config.GetDefaultNamespace();
            if (commandLine["n"] != null)
            {
                destinationDirectory = commandLine["n"];
            }
            if (commandLine["namespace"] != null)
            {
                destinationDirectory = commandLine["namespace"];
            }
            return destinationDirectory;
        }

        private static string GetDestinationFilePath(Arguments commandLine)
        {
            var destinationDirectory = Config.GetDefaultOutputFile();
            if (commandLine["o"] != null)
            {
                destinationDirectory = commandLine["o"];
            }
            if (commandLine["out"] != null)
            {
                destinationDirectory = commandLine["out"];
            }
            return destinationDirectory;
        }
        private static string GetDefaultInputFile(Arguments commandLine)
        {
            var defaultInputFile = Config.GetDefaultInputFile();
            if (commandLine["c"] != null)
            {
                defaultInputFile = commandLine["c"];
            }
            if (commandLine["csv"] != null)
            {
                defaultInputFile = commandLine["csv"];
            }
            return defaultInputFile;
        }
        private static void GetHelp(Arguments commandLine)
        {
            if (commandLine["h"] != null || commandLine["help"] != null)
            {
                Error(FormatCommandLineHelp());
            }
        }
        private static string FormatCommandLineHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Generate Class From Csv Help");
            sb.AppendLine();
            sb.AppendLine("Valid Commands:");
            sb.AppendLine("-a or --auto or no parameters Run the program using the parameters found in app.config");
            sb.AppendLine("-i or --interactive Prompt for program parameters");
            sb.AppendLine("-h or --help Display this help screen");
            sb.AppendLine("-c or --csv=\"{filepath}\" will read from the file at {filepath}");
            sb.AppendLine("-o or --out=\"{filepath}\" will direct the output to the file at {filepath} ");

            return sb.ToString();
        }
        /// <summary>
        /// Application error: display error and then stop conversion
        /// </summary>
        /// <param name="error">Error string</param>
        private static void Error(string error)
        {
            Console.WriteLine(error);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
