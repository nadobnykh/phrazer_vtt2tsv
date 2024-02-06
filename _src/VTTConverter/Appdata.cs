using System;
using System.IO;

namespace Phrazer
{

    class Appdata
    {
        public Appdata(){
        }
        public static string GetAppdataPath()
        {
            return Environment.CurrentDirectory + Path.DirectorySeparatorChar + "_appdata" + Path.DirectorySeparatorChar;
        }
    }
}
