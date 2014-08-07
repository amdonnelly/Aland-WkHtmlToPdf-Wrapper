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
    public class PublisherGroupCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new PublisherGroupElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((PublisherGroupElement)element).Name;
            }

            public override ConfigurationElementCollectionType CollectionType
            {
                get
                {
                    return ConfigurationElementCollectionType.BasicMap;
                }
            }

            protected override string ElementName
            {
                get
                {
                    return "publisher";
                }
            }

            protected override bool IsElementName(string elementName)
            {
                if (string.IsNullOrWhiteSpace(elementName) || elementName != "publisher")
                    return false;
                return true;
            }

            public PublisherGroupCollection this[int index]
            {
                get { return (PublisherGroupCollection)BaseGet(index); }
                set
                {
                    if (BaseGet(index) != null)
                        BaseRemoveAt(index);
                    BaseAdd(index, value);
                }
            }
        
    }
}
