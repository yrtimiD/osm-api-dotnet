using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OSM.API.v6
{
    public partial class Osm
    {
        [XmlElement(ElementName = "api")]
        public Api api
        {
            get;
            set;
        }

		[XmlIgnoreAttribute()]
		public bool apiSpecified { get; set; }
    }

    public partial class Api
    {
        [XmlElement(ElementName = "version")]
        public Version version { get; set; }
        [XmlElement(ElementName = "area")]
        public Area area { get; set; }
        [XmlElement(ElementName = "tracepoints")]
        public TracePoints tracepoints { get; set; }
        [XmlElement(ElementName = "waynodes")]
        public WayNodes waynodes { get; set; }
        [XmlElement(ElementName = "changesets")]
        public Changesets changesets { get; set; }
        [XmlElement(ElementName = "timeout")]
        public Timeout timeout { get; set; }
    }

    public partial class Version
    {
        [XmlAttribute]
        public string minimum { get; set; }

        [XmlAttribute]
        public string maximum { get; set; }
    }

    public partial class Area
    {
        [XmlAttribute]
        public double maximum { get; set; }
    }

    public partial class TracePoints
    {
        [XmlAttribute]
        public int per_page { get; set; }
    }

    public partial class WayNodes
    {
        [XmlAttribute]
        public int maximum { get; set; }
    }

    public partial class Changesets
    {
        [XmlAttribute]
        public int maximum_elements { get; set; }
    }

    public partial class Timeout
    {
        [XmlAttribute]
        public int seconds { get; set; }
    }


}
