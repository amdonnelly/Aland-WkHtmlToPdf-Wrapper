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
    public class PublisherValueElement : ConfigurationElement
        {

        public PublisherValueElement()
            {

            }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string name
            {
                get { return (string)base["name"]; }
                set { base["name"] = value; }
            }
        [ConfigurationProperty("value", IsKey = true, IsRequired = false)]
        public string value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }
        
    }
}
