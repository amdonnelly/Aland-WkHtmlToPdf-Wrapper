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
    public class ConfigurationSectionHandler : ConfigurationSection
    {

        [ConfigurationProperty("", IsRequired = false, IsKey = false, IsDefaultCollection = true)]
        public PublisherGroupCollection Items
        {
            get { return ((PublisherGroupCollection)(base[""])); }
            set { base[""] = value; }
        } 
    }
}
