using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketCore.Classes
{
    public class FilePath
    {
        [Key]
        public int FilePathId { get; set; }
        public ColorVariation ColorVariation { get; set; }
        public string Path { get; set; }
    }
}
