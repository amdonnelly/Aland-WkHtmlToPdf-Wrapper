using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlanD.wkhtmltopdf.Config
{
    /// <summary>
    /// Used for arguments in the final exe call
    /// 
    /// Only options with Enabled=true will be included
    /// </summary>
    class Option
    {


        public Boolean Enabled { get; set; }

        public string Key { get; set; }

        public string ShortKey { get; set; }

        public string Value { get; set; }
    }
}
