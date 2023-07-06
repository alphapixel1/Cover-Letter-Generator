using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.StaticClasses
{
    internal static class TempFiles
    {
        /*
         * Requires period in extension
         */
        public static string GetTempFileNameWithExtension(string extension)
        {
            string tempPath = Path.GetTempPath();
            string uniqueFileName = Guid.NewGuid().ToString("N");
            string tempFileName = Path.Combine(tempPath, uniqueFileName + extension);
            return tempFileName;
        }
    }
}
