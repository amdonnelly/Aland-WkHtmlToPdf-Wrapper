using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlanD.wkhtmltopdf
{
    /// <summary>
    /// Return the output of the PDFgenerator class
    /// </summary>
    public class PdfResult
    {
        public Byte[] Bytes { get; set; }
        public string FilePath { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
