using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Colorful;
using Console = Colorful.Console;

namespace SkypeAdRemover
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var skype = new[]
            {
                new Formatter("Skype", Color.Lime),
            };
            Console.WriteLineFormatted("{0}(c) Ad Remover ", Color.DarkGray, skype);
            Console.WriteLine("A XenocodeRCE software");
            Console.WriteLine("Copyright (c) 2016 - XenocodeRCE");
            Console.WriteLine("https://github.com/XenocodeRCE");
            Console.WriteLine();
            var star = new[]
            {
                new Formatter("*", Color.Lime),
            };
            var cross = new[]
            {
                new Formatter("     x", Color.Lime),
            };
            Console.WriteLineFormatted("{0} Scanning for Skype file ...", Color.DarkGray, star);

            var skypefolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\skype";

            if (Directory.Exists(skypefolder))
            {
                ProcessDirectory(skypefolder);
            }

            Console.WriteFormatted("{0} Re-Starting Skype Instance...", Color.DarkGray, cross);
            try
            {
                Process.Start("Skype.exe");
                Console.Write("<");
                Console.WriteFormatted("Done", Color.Lime);
                Console.Write(">");
                Console.WriteLine();
            }
            catch (Exception)
            {

                Console.Write("<");
                Console.WriteFormatted("Error", Color.Crimson);
                Console.Write(">");
                Console.WriteLine();
            }

            Console.WriteLine("", Color.Lime);
            Console.WriteLine("", Color.Lime);
            Console.WriteLine("", Color.Lime);
            Console.WriteLine("                     *******************************", Color.Lime);
            Console.WriteLine("                     * Skype Ads Has Been Removed !*", Color.Lime);
            Console.WriteLine("                     *******************************", Color.Lime);

            Console.ReadKey(true);
        }



        public static void ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }


        public static void ProcessFile(string path)
        {
            if (Path.GetFileName(path) != "config.xml") return;
            var cross = new[]
            {
                new Formatter("     x", Color.Lime),
            };
            Console.WriteFormatted("{0} Extracting config...", Color.DarkGray, cross);
            Console.Write("<");
            Console.WriteFormatted("Done", Color.Lime);
            Console.Write(">");
            Console.WriteLine();

            Console.WriteFormatted("{0} Closing Skype Instance...", Color.DarkGray, cross);
            //close skype
            var processes = Process.GetProcessesByName("Skype");
            if (processes.Length > 0)
            {
                processes[0].CloseMainWindow();
                Console.Write("<");
                Console.WriteFormatted("Done", Color.Lime);
                Console.Write(">");
                Console.WriteLine();
            }

            var text = File.ReadAllText(path);
            text = text.Replace("<AdvertPlaceholder>1</AdvertPlaceholder>", "<AdvertPlaceholder>0</AdvertPlaceholder>");
            Console.WriteFormatted("{0} Saving modification...", Color.DarkGray, cross);
            try
            {
                File.WriteAllText(path, text);
                Console.Write("<");
                Console.WriteFormatted("Done", Color.Lime);
                Console.Write(">");
                Console.WriteLine();
            }
            catch (Exception)
            {
                var newpath = Path.GetDirectoryName(path) + "config.xml";
                File.Delete(path);
                File.WriteAllText(newpath, text);
                Console.Write("<");
                Console.WriteFormatted("Done", Color.Lime);
                Console.Write(">");
                Console.WriteLine();
            }
        }
    }
}
