using System;
using System.IO;


namespace Phrazer
{
    class SubsDownloadHTMLBuilder
    {
        public static string GetDownloadPath()
        {
            return Appdata.GetAppdataPath() + "downloadSubs" + Path.DirectorySeparatorChar;
        }

        // Subtitles downloader (TAAHM from ling.online)
        public static void createDownloadSubtitlesHTML()
        {
            string remoteUri = "";
            string fileName = "";
            string destHtml = "";
            string destFolder = SubsDownloadHTMLBuilder.GetDownloadPath();

            // SEASON
            for(int s = 1; s < 13; s++) {
                // EPISODE
                for(int e = 1; e < 26; e++) {
                    remoteUri = "https://media.ling.online/media/filebrowser/video/series/two-and-a-half-men/"+s+"/"+s+"-"+e+"/subtitles/en.vvt";
                    fileName = "Two and a Half Men (S" + s.ToString().PadLeft(2, '0') + " E" + e.ToString().PadLeft(2, '0') + ").vtt";

                    destHtml = destHtml + "<a href=\""+remoteUri+"\" download=\""+fileName+"\">"+fileName+"</a><br/>" + Environment.NewLine;
                    Console.WriteLine(remoteUri);
                }
            }
            File.WriteAllText(destFolder+"TAAHM_Subs.html", destHtml);
        }

    }
}
