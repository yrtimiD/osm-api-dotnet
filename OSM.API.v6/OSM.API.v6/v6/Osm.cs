using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace OSM.API.v6
{
	[Serializable]
	[XmlRoot("osm", Namespace = "", IsNullable = false)]
	public partial class Osm
	{
		[XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Bound bound { get; set; }

		[XmlElement("node", typeof(Node), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public List<Node> Nodes { get; set; }

		[XmlElement("way", typeof(Way), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public List<Way> Ways { get; set; }

		[XmlElement("relation", typeof(Relation), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public List<Relation> Relations { get; set; }

		[XmlAttribute]
		public string version { get; set; }

		[XmlAttribute]
		public string generator { get; set; }

		[XmlAttribute]
		public bool upload { get; set; }
		public bool uploadSpecified { get; set; }
	}

    [Serializable]
    public partial class Bound
    {
        [XmlAttribute]
        public string origin
        {
            get;
            set;
        }

        [XmlAttribute]
        public string box
        {
            get;
            set;
        }
    }

    [Serializable]
    public partial class Nd
    {
        [XmlAttribute]
        public long @ref
        {
            get;
            set;
        }
    }

	[XmlInclude(typeof(Node))]
	[XmlInclude(typeof(Way))]
	[XmlInclude(typeof(Relation))]
	[System.Diagnostics.DebuggerDisplay("id: {id}, version: {version}")]
	public partial class Element
	{
		[XmlAttribute]
		public long id { get; set; }

		[XmlAttribute]
		public bool visible { get; set; }

		[XmlIgnoreAttribute()]
		public bool visibleSpecified { get; set; }

		[XmlAttribute]
		public string user { get; set; }

		[XmlIgnoreAttribute()]
		public bool userSpecified { get; set; }

		[XmlAttribute]
		public long uid { get; set; }

		[XmlIgnoreAttribute()]
		public bool uidSpecified { get; set; }

		[XmlAttribute]
		public System.DateTime timestamp { get; set; }

		[XmlIgnoreAttribute()]
		public bool timestampSpecified { get; set; }

		[XmlAttribute]
		public int version { get; set; }

		[XmlIgnoreAttribute()]
		public bool versionSpecified { get; set; }

		[XmlAttribute]
		public long changeset { get; set; }

		[XmlIgnoreAttribute()]
		public bool changesetSpecified { get; set; }

		[XmlAttribute]
		public string action { get; set; }

		[XmlIgnoreAttribute()]
		public bool actionSpecified { get; set; }
	}

	[Serializable]
	public partial class Way : Element
	{
		[System.Xml.Serialization.XmlElementAttribute("nd", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Nd[] nds
		{
			get
			{
				if (Nodes == null) return null;
				return this.Nodes.ToArray();
			}
			set
			{
				this.Nodes = value == null ? new List<Nd>() : value.ToList();
			}
		}

		[System.Xml.Serialization.XmlElementAttribute("tag", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Tag[] tags
		{
			get
			{
				if (Tags == null) return null;

				var t = this.Tags.Select(kvp => new Tag { k = kvp.Key, v = kvp.Value });
				return t.ToArray();
			}
			set
			{
				this.Tags = value == null ? new Dictionary<String, String>() : value.ToDictionary(t => t.k, t => t.v);
			}
		}
    
		[XmlIgnore]
		public Dictionary<String, String> Tags { get; set; }

		[XmlIgnore]
		public List<Nd> Nodes { get; set; }

	}

    [Serializable]
    public partial class Tag
    {
        [XmlAttribute]
        public string k
        {
            get;
            set;
        }

        [XmlAttribute]
        public string v
        {
            get;
            set;
        }
    }

    [Serializable]
    public partial class Member
    {
        [XmlAttribute]
        public MemberType type
        {
            get;
            set;
        }

        [XmlAttribute]
        public long @ref
        {
            get;
            set;
        }

        [XmlAttribute]
        public string role
        {
            get;
            set;
        }
    }

    [Serializable]
    public enum MemberType
    {
        way,
        node,
        relation,
    }

    [Serializable]
    public partial class Relation:Element
    {
        [XmlElement("member", typeof(Member), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("tag", typeof(Tag), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object[] Items
        {
            get;
            set;
        }
    }

	[Serializable]
	public partial class Node : Element
	{
		[XmlElement("tag", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Tag[] tag { get; set; }

		[XmlAttribute]
		public double lat { get; set; }

		[XmlAttribute]
		public double lon { get; set; }
	}
}