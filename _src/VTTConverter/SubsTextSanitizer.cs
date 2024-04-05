using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Phrazer
{
    class SubsTextSanitizer
    {
        
        public static string SanitizeText(string text)
        {
            text = Regex.Replace(text, @"\s+", " ");
            text = RemoveBracketsText(text, '[', ']');
            text = RemoveBracketsText(text, '(', ')');
            text = text.Trim();

            // If subtitle row number detected. Just Skip
            if (text.Length < 5 && Int32.TryParse(text, out int numValue))  return "";

            text = text.Replace("&lt;i>", "");
            text = text.Replace("&lt;/i>", "");
            text = text.Replace("&lt;br/>", " ");
            text = text.Replace("\t", "");
            text = text.Replace("\"", "");
            text = text.Replace("“", "");
            text = text.Replace("”", "");
            text = text.Replace("<i>", "");
            text = text.Replace("</i>", "");
            text = text.Replace(" !", "!");
            text = text.Replace(" ?", "?");
            text = text.Replace(" !", "!");
            text = text.Replace(" ?", "?");
            text = text.Replace("I-I", "I");
            text = text.Replace("’", "'");
            text = text.Replace("!", ".");
            text = text.Replace("<", " ");
            text = text.Replace(">", " ");
            text = text.TrimStart('-');
            
            // Remove all "NAMES:"  
            if(IsAllUpper(text.Split()[0].Replace(":","")) && text.Contains(":")) {
                text = RemoveBracketsText(text, text[0], ':');
            }

            text = text.Replace(":", ".");
            text = text.Replace(";", ",");

            text = SanitizePrefix(text);
            text = SanitizeMiddle(text);
            text = text.Trim();
            return text;
        }

        static string SanitizePrefix(string text)
        {
            text = text.Trim();
            if(text.Length == 0) return text;

            string[] suffixSigns = {
                "...", ".", ","
            };

            string[] strings = {
                // DELETE ANYWAY
                "Wow", "wow",
                "Whoa", "Mmm", "Nah", "Aw",
                "Mm", "mm",
                "Hmm", "hmm",
                "Blah", "blah",
                "Ah", "ah",
                "Oh", "oh",
                "Uh", "uh",
                "Um", "um",
                "Ooh", "ooh",
                //"Hey", "hey",
                //"Hi", "hi",
                //"Yeah", "yeah",
                //"Okay", "okay",
                //"Well", "well",
                //"So", "so",
                
                // DELETE MAYBE
                /*
                "Yes", "yes",
                "No", "no",
                "Now", "now",
                "And", "and",
                "But", "but",
                "Listen", "listen",
                "Look", "look",
                "Gee", "Boy",
                "Darling", "Sweetheart",
                */

                // DELETE NAMES
                "Alan", "Alan",
                "Charlie", "Charlie",
                "Judith", "Rose", "Jake", "Berta", "Lyndsey", "Frankie", "Mom", "Naomi"
            };

            int affected = 0;
            bool fixCase = false;
            for(int t = 0; t < 10; t++) {
                affected = 0;
                for(int i = 0; i < strings.Length; i++) {
                    for(int s = 0; s < suffixSigns.Length; s++) {
                        string pattern = strings[i] + suffixSigns[s] + " ";
                        if(text.StartsWith(pattern)) {
                            text = text.Substring(pattern.Length).Trim();
                            fixCase = true;
                            affected++;
                        }
                    }
                }
                if(text.Length == 0) return text;

                // Dont forget to fix case after cutoff
                if(fixCase) text = text.First().ToString().ToUpper() + text.Substring(1);
                if(affected == 0) break;
            }
            return text;
        }

        static string SanitizeMiddle(string text)
        {
            if(text.Length == 0) return text;

            string[] strings = {
                "ah,",
                "oh,",
                "uh,",
                "um,",
                "well,",

                "Alan.", "Lyndsey.", "Charlie.", "Mom.", "Walden Smith.", "Sam.", "Monkey Man."
            };

            string pattern = "";
            int affected = 0;
            for(int t = 0; t < 10; t++) {
                affected = 0;
                for(int i = 0; i < strings.Length; i++) {
                    pattern = ", " + strings[i];
                    if(text.Contains(pattern)) {
                        text = text.Replace(pattern, strings[i].Substring(strings[i].Length - 1));
                        affected++;
                    }
                }
                if(text.Length == 0) return text;
                if(affected == 0) break;
            }
            return text;
        }





        static bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }



        static string RemoveBracketsText(string text, char startsWith, char endsWith)
        {
            int firstBracket = text.IndexOf(startsWith);
            int lastBracket = text.LastIndexOf(endsWith);

            if(firstBracket == -1) return text;
            if(lastBracket == -1) return text;

            int diff = lastBracket - firstBracket + 1;
            return text.Remove(firstBracket, diff);
        }


        public static string SanitizeVocabulary(string text)
        {
            
            text = text.Replace("!", "");
            text = text.Replace("?", "");
            text = text.Replace(",", "");
            text = text.Replace(".", "");
            text = text.Replace("--", "");

            text = text.Trim();

            if(Regex.IsMatch(text, @"[^a-zA-Z,\s'-]+", RegexOptions.IgnoreCase))
            return "";

            if(text == "I" || text == "I'm" || text == "I've" || text == "I'd" || text == "I'll") return text;

            return text.ToLower();
        }
        
    }
}
