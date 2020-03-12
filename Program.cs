using System;
using System.IO;

namespace HDtoSD
{
    class Program
    {
        //Program arguments
        private const bool replace = true;
        private const bool copyEmpty = true;

        //Rainbow mode variable
        private const bool coloring = true;

        // Resize Scale
        private const float ResizeScale = 0.5f;

        // File Filter Pattern
        private const string HighResPattern = "@2x";
        static void Main(string[] args)
        {
            ConsoleColor startColor = Console.ForegroundColor;

            FileLoader fileLoader = new FileLoader(Directory.GetCurrentDirectory(), HighResPattern, ImageFormat.Jpg, ImageFormat.Png);
            var files = fileLoader.Load();

            //Displays information
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Replacing: " + replace);
            Console.WriteLine("Copy empty images: " + copyEmpty);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(files.Count + " files will be generated");
            Console.WriteLine("\nPress C to cancel or any other key to continue");

            //Checks user input
            if (Console.ReadKey().Key == ConsoleKey.C)
            {
                //Stops the program
                return;
            }
            Array colors = Enum.GetValues(typeof(ConsoleColor));

            //Resizes the images
            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                //Changes the color if rainbow mode is on
                if (coloring)
                {
                    Console.ForegroundColor = (ConsoleColor)colors.GetValue(i % 16);
                }

                //Outputs and resizes the file
                Console.WriteLine(file);
                ResizeImage.Resize(file, file.Replace(HighResPattern, string.Empty), ResizeScale);
            }

            //Finishes the program
            Console.ForegroundColor = startColor;
            Console.WriteLine("\n\nFile generation finished, press any key to exit the program");
            Console.ReadKey();
        }

#if WIP
        private static void DisplayHelp()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("osu! HD skin to SD converter");
            Console.WriteLine("Code written by DarkBird (https://osu.ppy.sh/users/8042593)");
            Console.WriteLine("Usage: HDtoSD.exe [parameters]");
            Console.WriteLine("\n\n\nOptional parameters:");
            Console.WriteLine("-r   --replace + true/false     If set to true the already existing SD images will be overwritten");
            Console.WriteLine("-cp  --copy                     Copies the empty images (1x1 pixel size) instead of renaming them");
            Console.WriteLine("-c   --color                    Activates rainbow mode (just for fun)");
            Console.WriteLine("-h   --help                     Displays help information");
            Console.WriteLine("\n\n-----------------------------------------");
        }

        public static void ArgumentsParsing()
        {
            //Handles arguments
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    //Checks the current parameter
                    if (args[i] == "-r" || args[i] == "--replace")
                    {
                        //Tries to parse the given parameter to a boolean value
                        if (!bool.TryParse(args[i + 1], out replace))
                        {
                            //Error handling
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Couldn't set replace parameter, using default option");
                            Console.ForegroundColor = ConsoleColor.White;
                            replace = false;
                        }
                        i++;
                    }
                    else if (args[i] == "-c" || args[i] == "--color")
                    {
                        coloring = true;
                    }
                    else if (args[i] == "-cp" || args[i] == "--copy")
                    {
                        copyEmpty = true;
                    }
                    else if (args[i] == "-h" || args[i] == "--help")
                    {
                        DisplayHelp();
                        return;
                    }
                    else
                    {
                        //Error handling
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Unkown parameter " + args[i]);
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                }
            }
        }
#endif
    }
}
