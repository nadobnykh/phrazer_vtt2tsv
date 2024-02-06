using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Phrazer
{
    class SubsHelper
    {
        public static string GetInputPath()
        {
            return Appdata.GetAppdataPath() + "input" + Path.DirectorySeparatorChar;
        }

        public static string GetOutputAbsoluteFilename(string currentFileName, string suffix)
        {
            string path = Appdata.GetAppdataPath() + "output" + Path.DirectorySeparatorChar;
            if(!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path + Path.GetFileNameWithoutExtension(currentFileName) + suffix + ".tsv";
        }



        public static string GetOutputTime(string text)
        {
            text = text.Trim();
            string muster = "03:00.242 --> 03:03.002";
            if(text.Contains(" --> ") && text.Length == muster.Length) {
                return "00:" + text.Substring(0, 5);
            }

            string muster1_1 = "1:03:00.242 --> 1:03:03.002";
            if(text.Contains(" --> ") && text.Length == muster1_1.Length) {
                return "0" + text.Substring(0, 7);
            }

            string muster1_2 = "59:58.512 --> 1:00:00.472";
            if(text.Contains(" --> ") && text.Length == muster1_2.Length) {
                return "00:" + text.Substring(0, 5);
            }

            string muster2 = "00:00:20.729 --> 00:00:22.731";
            if(text.Contains(" --> ") && text.Length == muster2.Length) {
                return text.Substring(0, 8);
            }

            return "";
        }

        
        
        public static string GetWordCountString(string text)
        {
            return ("" + GetWordsCount(text)).PadLeft(2, '0');
        }

        public static int GetWordsCount(string text)
        {
            return text.Split(" ").Length;
        }
    }
}
