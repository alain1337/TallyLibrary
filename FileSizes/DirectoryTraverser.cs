using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSizes
{
    public class DirectoryTraverser
    {
        public delegate void OnDirectoryHandler(DirectoryInfo info);
        public delegate void OnFileHandler(FileInfo info);

        public event OnDirectoryHandler OnDirectory;
        public event OnFileHandler OnFile;

        public DirectoryTraverserResult Traverse(string startDirectory)
        {
            var result = new DirectoryTraverserResult { StartDirectory = startDirectory };
            Traverse(startDirectory, result);
            result.EndTime = DateTime.Now;
            return result;
        }

        void Traverse(string directory, DirectoryTraverserResult result)
        {
            try
            {
                var dirInfo = new DirectoryInfo(directory);
                result.Directories++;
                OnDirectory?.Invoke(dirInfo);

                if (OnFile != null)
                    foreach (var file in dirInfo.EnumerateFiles())
                        OnFile.Invoke(file);

                foreach (var dir in dirInfo.EnumerateDirectories())
                    Traverse(Path.Combine(directory, dir.Name), result);
            }
            catch
            {
                result.Exceptions++;
            }
        }
    }

    public class DirectoryTraverserResult
    {
        public string StartDirectory { get; internal set; }
        public int Directories { get; internal set; }
        public int Files { get; internal set; }
        public int Exceptions { get; internal set; }
        public DateTime StartTime { get; } = DateTime.Now;
        public DateTime EndTime { get; internal set; } = DateTime.Now;

        public TimeSpan Elapsed => EndTime - StartTime;
    }
}
