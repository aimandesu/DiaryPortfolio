using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Request
{
    public class MediaStream
    {
        public Stream Stream { get; set; } = Stream.Null; // file content
        public string FileName { get; set; } = string.Empty; // original name for path & extension
    }
}
