using System.IO;

namespace nrnUtil
{
    /// <summary>
    /// Directory size class with recursive sub directory inclusion
    /// 
    /// (c) Copyright Vincent Wochnik 2007
    /// </summary>
    public class DirectorySize
    {
        /// <summary>
        /// Returns the size of a directory
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <param name="includeSubDirectories">Specifyes, wheather sub directories are included</param>
        /// <returns>Directory size</returns>
        public static long GetDirectorySize(string path, bool includeSubDirectories)
        {
            long size = 0;

            // get sub directories (recursive)
            if (includeSubDirectories)
            {
                try
                {
                    string[] subDirectories = System.IO.Directory.GetDirectories(path);
                    foreach (string subDirectory in subDirectories)
                        size += GetDirectorySize(subDirectory, includeSubDirectories);
                }
                catch { /* what should we do??? */ }
            }

            // get files and add size
            try
            {
                string[] fileNames = System.IO.Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    size += fileInfo.Length;
                }
            }
            catch { /* what should we do??? */ }

            return size;
        }
    }
}