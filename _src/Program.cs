using System;
using System.IO;
using System.Text;

namespace Phrazer
{
    class Program
    {
        static void Main(string[] args)
        {
            //VTTConverter.createDownloadSubtitlesHTML(); return;
            VTTConverter.convertToTSV();
        }
    }
}
