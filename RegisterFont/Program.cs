using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace RegisterFont
{
    class Program
    {
        [DllImport("gdi32.dll")]
        static extern bool RemoveFontResource(string lpFileName);
        
        [DllImport("gdi32.dll")]
        static extern int AddFontResource(string lpFilename);

        static void Main(string[] args)
        {
            Console.WriteLine("RegisterFont - temp. registers fonts as normal user\r\n");

            if (args.Length < 1)
                return;

            switch (args[0].ToLower())
            {
                case "add":
                case "rem":
                    PerformAction(args);
                    break;
                case "help":
                    ShowHelp();
                    break;
                default:
                    return;
            }
            Console.WriteLine("\r\nAll pending jobs done!");
        }

        public static void PerformAction(string[] strFilesToProcess)
        {
            for (int i = 1; i <= strFilesToProcess.GetUpperBound(0); i++)
            {
                FileInfo objFileInfo = new FileInfo(strFilesToProcess[i]);

                if (!objFileInfo.Exists)
                {
                    Console.WriteLine("Can't find specified font file: {0}", objFileInfo.Name.ToString());
                }
                else
                {
                    if (strFilesToProcess[0] == "add".ToLower())
                    {
                        AddFontResource(strFilesToProcess[i]);
                        Console.WriteLine("Done adding {0}.", strFilesToProcess[i]);
                    }
                    else if (strFilesToProcess[0] == "rem".ToLower())
                    {
                        RemoveFontResource(strFilesToProcess[i]);
                        Console.WriteLine("Done removing {0}.", strFilesToProcess[i]);
                    }
                }

                objFileInfo = null;
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("\r\nUsage:");
            Console.WriteLine("   {0} [ add|rem ] <font1.ttf> <font2.ttf> ... <font n.ttf>", "RegisterFont.exe");
            Console.WriteLine("\r\nNote:");
            Console.WriteLine("   Changes won't persist, when rebooting the changes made by\r\n   this application are gone.");
        }
    }
}