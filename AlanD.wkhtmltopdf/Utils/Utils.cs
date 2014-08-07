using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using System.IO;


namespace AlanD.wkhtmltopdf.Utils
{
    public static class Utils
    {

        public static string ResolveFilePath(string _path)
        {
            String _return = _path;

            if (_path.StartsWith("~/"))
            {
                _return = _path.Replace("~/", "").Replace("/", "\\");
                _return = System.IO.Path.GetFullPath(System.IO.Path.Combine(@Thread.GetDomain().BaseDirectory, @_return));
            }

            else
            {
                _return = System.IO.Path.GetFullPath(System.IO.Path.Combine(@Thread.GetDomain().BaseDirectory, @_return));
            }

            Uri _uri = new Uri(_return);
            if (!_uri.IsFile)
            {
                if (!_return.EndsWith("\\"))
                {
                    _return = _return + "\\";
                }
            }

            return _return;
        }
    }
}
