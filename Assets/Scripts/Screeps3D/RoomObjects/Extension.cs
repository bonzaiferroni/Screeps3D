namespace Screeps3D {
    
    /*{
      "_id": "5a0f286dc63095000155ec62",
      "type": "extension",
      "x": 11,
      "y": 23,
      "room": "E2S7",
      "notifyWhenAttacked": true,
      "user": "5a0da017ab17fd00012bf0e7",
      "energy": 50,
      "energyCapacity": 50,
      "hits": 1000,
      "hitsMax": 1000,
      "off": false
    }*/
    
    public class Extension : Structure, IEnergyObject {
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