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
    public class PublisherGroupElement : ConfigurationElement
        {

            [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
            [StringValidator(InvalidCharacters = @" ~.!@#$%^&*()[]{}/;'""|\")]
            public string Name
            {
                get { return (string)base["name"]; }
                set { base["name"] = value; }
            }



            [ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
            public PublisherValueGroup Values
            {
                get { return (PublisherValueGroup)base[""]; }
            }
        
    }
}
