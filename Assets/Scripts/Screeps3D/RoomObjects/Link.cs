using System.Collections.Generic;

namespace Screeps3D
{
    /*{
        "_id":"594816d4dc09e4106f2b782f",
        "type":"link",
        "x":20,
        "y":8,
        "room":"W8S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "energy":779,
        "energyCapacity":800,
        "cooldown":0,
        "hits":1000,
        "hitsMax":1000,
        "actionLog":{
            "transferEnergy":null
        }
    }*/
    public class Link : Structure, IEnergyObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }
        public Dictionary<string, JSONObject> Actions { get; set; }


        internal Link()
        {
            Actions = new Dictionary<string, JSONObject>(); 
        }

        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);

            UnpackUtility.Energy(this, data);
            
            var actionObj = data["actionLog"];
            if (actionObj != null)
            {
                foreach (var key in actionObj.keys)
                {
                    var actionData = actionObj[key];
                    Actions[key] = actionData;
                }
            }
        }
    }
}