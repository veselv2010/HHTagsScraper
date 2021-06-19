using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HHTagsScraper
{
    public sealed class LocalDirectoryNameResolver
    {
        private string _path;
        public LocalDirectoryNameResolver(string path)
        {
            _path = path;
        }

        public IEnumerable<string> GetFolderNames()
        {
            if (!Directory.Exists(_path!))
            {
                throw new IOException();
            }

            return new DirectoryInfo(_path).GetDirectories()
                .Select((x) => x.Name);
        }
    }
}
