using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace OSM.API.v6
{
    public class Proxy
    {
        //static readonly string API_URL = @"http://api06.dev.openstreetmap.org";
        static readonly string API_URL = @"http://api.openstreetmap.org";
        static readonly string CAPABILITIES = "/api/capabilities";
        static readonly string API_PREFIX = "/api/0.6/";

		public Api Capabilities { get; private set; }

        public Proxy()
        {
            this.Capabilities = GetCapabilities();
        }

        public Api GetCapabilities()
        {
            Uri u = new Uri(API_URL+CAPABILITIES);
            Osm result = GetOsmFromApi(u);

            return result.api;
        }

		public IEnumerable<Node> GetNodes(IEnumerable<long> ids)
		{
			Osm osm = GetMany("nodes", ids);
			return osm.Nodes;
		}
		public Osm GetWaysOsm(IEnumerable<long> ids)
		{
			Osm osm = GetMany("ways", ids);
			return osm;
		}

		public IEnumerable<Way> GetWays(IEnumerable<long> ids)
		{
			Osm osm = GetMany("ways", ids);
			return osm.Ways;
		}

		public IEnumerable<Relation> GetRelations(IEnumerable<long> ids)
		{
			Osm osm = GetMany("relations", ids);
			return osm.Relations;
		}

		private Osm GetMany(string type, IEnumerable<long> ids)
        {
            String u = API_URL + API_PREFIX;
            
            string vector = ids.Select(i=>i.ToString()).Aggregate((s1,s2)=>s1+","+s2);

            u += String.Format("{0}?{0}={1}", type, vector);

            Osm result = GetOsmFromApi(new Uri(u));
            return result;
        }

        private static Osm GetOsmFromApi(Uri uri)
        {
			//if (uri.ToString().Length > 2000) throw new ArgumentException("Uri is too long");
			Stream stream = null;
			String data = null;
			WebClient client = new WebClient();
			try
			{
				stream = client.OpenRead(uri);
				data = new StreamReader(stream).ReadToEnd();
			}
			catch (WebException e)
			{
				HttpWebResponse httpResponse = e.Response as HttpWebResponse;
				if (httpResponse != null)
				{
					if (httpResponse.StatusCode == HttpStatusCode.NotFound)
					{
						throw new ArgumentException("Not found");
					}
					//todo: add other known error types
				}

				throw new ArgumentException("Can't get result from API", e);
			}

			try{
                XmlSerializer ser = new XmlSerializer(typeof(Osm));
				TextReader reader = new StringReader(data);
				Osm result = (Osm)ser.Deserialize(reader);
                return result;
            }
            catch (Exception e)
            {
                Exception ex = new ApplicationException("Can't deserialize Osm object", e);
				ex.Data["xml"] = data;
				throw ex;
            }
        }
    }
}
