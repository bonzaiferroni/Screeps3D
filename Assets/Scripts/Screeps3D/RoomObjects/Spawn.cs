namespace Screeps3D {
    
    /*{
      "_id": "5a0da460ab17fd00012bf0e9",
      "type": "spawn",
      "room": "E2S7",
      "x": 15,
      "y": 19,
      "name": "Spawn1",
      "user": "5a0da017ab17fd00012bf0e7",
      "energy": 9,
      "energyCapacity": 300,
      "hits": 5000,
      "hitsMax": 5000,
      "spawning": {
        "name": "E2S7s_E1S7_swarmMiner0_60",
        "needTime": 21,
        "remainingTime": 12
      },
      "notifyWhenAttacked": true,
      "off": false
    }*/
    
    public class Spawn : Structure, IEnergyObject {
        public float Energy { get; private set; }
        public float EnergyCapacity { get; private set; }
        
        internal override void Unpack(JSONObject data) {
            base.Unpack(data);
            var energyObj = data["energy"];
            if (energyObj != null) {
                Energy = energyObj.n;
            }

            var energyCapacityObj = data["energyCapacity"];
            if (energyCapacityObj) {
                EnergyCapacity = energyCapacityObj.n;
            }
        }
    }
}