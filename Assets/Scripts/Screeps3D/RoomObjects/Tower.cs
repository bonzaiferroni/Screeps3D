using System.Collections.Generic;

namespace Screeps3D.RoomObjects
{
    /*{
        "_id":"5945134eea485cae18c518ef",
        "type":"tower",
        "x":7,
        "y":24,
        "room":"W8S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "energy":990,
        "energyCapacity":1000,
        "hits":3000,
        "hitsMax":3000,
        "actionLog":{
            "attack":null,
            "heal":null,
            "repair":null
        }
    }*/

    public class Tower : Structure, IEnergyObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }
        public Dictionary<string, JSONObject> Actions { get; set; }


        internal Tower()
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