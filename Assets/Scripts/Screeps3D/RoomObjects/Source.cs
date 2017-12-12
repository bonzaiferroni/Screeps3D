namespace Screeps3D.RoomObjects
{
    /* {
      "_id": "5a0b5e99ab17fd00012befed",
      "room": "E2S7",
      "type": "source",
      "x": 31,
      "y": 10,
      "energy": 0,
      "energyCapacity": 3000,
      "ticksToRegeneration": 300,
      "invaderHarvested": 62818,
      "nextRegenerationTime": 8529
    }*/

    public class Source : RoomObject, IEnergyObject, IRegenerationObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }
        public float NextRegenerationTime { get; set; }

        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);
            UnpackUtility.Energy(this, data);
            UnpackUtility.Regeneration(this, data);
        }
    }
}