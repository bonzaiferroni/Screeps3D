using System.Collections.Generic;

namespace Screeps3D
{
    /*
        "body":[
            {
                "type":"work",
                "hits":100
            },
            {
                "type":"work",
                "hits":100
            },
            {
                "type":"carry",
                "hits":100
            },
            {
                "type":"carry",
                "hits":100
            },
            {
                "type":"move",
                "hits":100
            }
        ],*/

    public class CreepBody
    {
        public List<CreepPart> Parts { get; private set; }

        public CreepBody()
        {
            Parts = new List<CreepPart>();
        }

        internal void Unpack(JSONObject data)
        {
            var bodyObj = data["body"];
            if (bodyObj == null)
                return;

            Parts.Clear();
            foreach (var partObj in bodyObj.list)
            {
                Parts.Add(new CreepPart
                {
                    hits = partObj["hits"].n,
                    type = partObj["type"].str,
                });
            }
        }
    }

    public class CreepPart
    {
        public string type;
        public float hits;
    }
}