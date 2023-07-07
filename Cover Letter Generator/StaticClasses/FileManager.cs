using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.StaticClasses
{
    internal class FileManager
    {
        public static string appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string LocalStorageFolder
        {
            get
            {
                var p = appDataFolderPath + "\\CoverLetterGenerator";
                if (!Directory.Exists(p))
                    Directory.CreateDirectory(p);
                return p;
            }
        }

        public static string GetAppDirectory(string v)
        {
            var p = LocalStorageFolder + "\\" + v;
            if (Directory.Exists(p))
                return p;
            Directory.CreateDirectory(p);
            return p;
        }


        public static string GetVideosFolderPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        public static string GetDownloadFolderPath() => Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();

        public static void ShowInExplorer(string path) => Process.Start("explorer.exe", $"/select,\"{path}\"");



        public static string CleanFilename(string filename)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalidChars)
            {
                filename = filename.Replace(c.ToString(), "");
            }
            return filename;
        }
        public static string GetUnusedFilename(string filePath, string directoryPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath);
            var output = Path.Combine(directoryPath, Path.GetFileName(filePath));
            int i = 1;
            while (File.Exists(output))
            {
                string tempFileName = string.Format("{0} ({1})", fileName, i++);
                output = Path.Combine(directoryPath, tempFileName + ext);
            }
            return output;
        }

        public static string GetRandomUnusedFilename(string directoryPath, string extension)
        {
            while (true)
            {
                string randomFilename = Path.Combine(directoryPath, Path.GetRandomFileName() + extension);
                if (!File.Exists(randomFilename))
                {
                    return randomFilename;
                }
            }
        }
        public static string GetRandomUnusedDirectoryName(string startDir)
        {
            string randDir;
            Random r = new Random();
            do
            {
                randDir = Path.Combine(startDir, Path.GetRandomFileName());
            }
            while (Directory.Exists(randDir));
            return randDir;
        }
    }
}
