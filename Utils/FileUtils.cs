using System;
using System.IO;
using System.Text;

namespace Auxiliary
{
    /// <summary>
    /// Simplified file management
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Save a file. If exists new file will be added
        /// </summary>
        /// <param name="path">Path and name</param>
        /// <param name="content">Content</param>
        public static void SaveFile(string path, string content)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, content);
            }
            else
            {
                string newFileName = SetNumber(Path.GetFileNameWithoutExtension(path));
                string newPath = string.Concat(Path.GetDirectoryName(path),@"\", newFileName, Path.GetExtension(path));
                SaveFile(newPath, content);            
            }
        }

        /// <summary>
        /// Save a file. If exists new file will be added. Extended
        /// </summary>
        /// <param name="DirName">Directory name</param>
        /// <param name="FileName">File name</param>
        /// <param name="content">Content</param>
        /// <param name="ClearFileName">Is clean char which not supported.</param>
        public static void SaveFile(string DirName, string FileName, string content, bool ClearFileName)
        {
            if (ClearFileName)
            {
                FileName = ParsFileName(FileName);
            }
            string Path = string.Format(@"{0}\{1}.xml", DirName, FileName);
            SaveFile(Path, content);
        }

       

        /// <summary>
        /// Get file name without Windows-unsupported symbols.
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <returns></returns>
        public static string ParsFileName(string filename)
        {
            System.Text.RegularExpressions.Regex R = new System.Text.RegularExpressions.Regex(@"[\\/:*?\""<>|]+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return R.Replace(filename,"");
        }
    }
}
