using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Xml;

namespace AlanD.wkhtmltopdf.Config
{
    /// <summary>
    /// Used to parse web.config section
    /// </summary>
    public class PublisherValueGroup : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new PublisherValueElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((PublisherValueElement)element).name;
            }
        
    }
}
