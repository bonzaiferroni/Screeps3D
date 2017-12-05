namespace Screeps3D
{
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

    public class Spawn : Structure, IEnergyObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }

        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);

            UnpackUtility.Energy(this, data);
        }
    }
}