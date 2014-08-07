using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlanD.wkhtmltopdf;

namespace AlanD.EventHandlers
{
    public delegate void OnStartEventHandler(PdfGenerator sender);

    public delegate void OnProgressEventHandler(PdfGenerator sender, String _message);

    public delegate void OnErrorEventHandler(PdfGenerator sender, String _message);

    public delegate void OnEndEventHandler(PdfGenerator sender);
}
