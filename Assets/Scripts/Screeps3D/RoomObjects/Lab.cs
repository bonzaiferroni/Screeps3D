using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D.RoomObjects
{
    /*{
        "_id":"594a260005d0cca1799cfc76",
        "type":"lab",
        "x":10,
        "y":19,
        "room":"W8S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "hits":500,
        "hitsMax":500,
        "mineralAmount":0,
        "cooldown":0,
        "mineralType":null,
        "mineralCapacity":3000,
        "energy":1080,
        "energyCapacity":2000,
        "actionLog":{
            "runReaction":null || "runReaction":{"x1":19,"y1":31,"x2":18,"y2":32}
        }
    }*/
    public class Lab : Structure, IEnergyObject, IActionObject, IResourceObject, ICooldownObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }
        public float ResourceAmount { get; set; }
        public float ResourceCapacity { get; set; }
        public string ResourceType { get; set; }
        public float Cooldown { get; set; }
        public Dictionary<string, JSONObject> Actions { get; set; }

        public Lab()
        {
            Actions = new Dictionary<string, JSONObject>();
        }
        
        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);

            var minAmountData = data["mineralAmount"];
            if (minAmountData != null)
            {
                ResourceAmount = minAmountData.n;
            }
            
            var minCapacityData = data["mineralCapacity"];
            if (minCapacityData != null)
            {
                ResourceCapacity = minCapacityData.n;
            }

            var minTypeData = data["mineralType"];
            if (minTypeData != null)
            {
                ResourceType = minTypeData.str;
            }

            UnpackUtility.Cooldown(this, data);
            UnpackUtility.Energy(this, data);
            UnpackUtility.ActionLog(this, data);
        }
    }
}