using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCore.Classes
{
    public class FilePath
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public FilePath(string path)
        {
            Path = path;
        }
    }
}
