namespace Screeps3D.RoomObjects
{
    /*{
      "_id": "5a0b5e99ab17fd00012befef",
      "type": "mineral",
      "mineralType": "H",
      "density": 4,
      "mineralAmount": 100000,
      "x": 25,
      "y": 28,
      "room": "E2S7",
      "nextRegenerationTime": 4049238
    }*/

    public class Mineral : RoomObject, IResourceObject, IRegenerationObject
    {
        public float ResourceAmount { get; set; }
        public float ResourceCapacity { get; set; }
        public string ResourceType { get; set; }
        public float NextRegenerationTime { get; set; }
        
        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);            
            UnpackUtility.Regeneration(this, data);
            
            var densityData = data["density"];
            if (densityData != null)
            {
                var density = densityData.n;
                ResourceCapacity = Constants.MINERAL_DENSITY.ContainsKey(density)
                    ? Constants.MINERAL_DENSITY[density]
                    : 0;
            }
            
            var mineralType = data["mineralType"];
            if (mineralType != null)
            {
                ResourceType = mineralType.str;
            }
            
            var minAmountData = data["mineralAmount"];
            if (minAmountData != null)
            {
                ResourceAmount = minAmountData.n;
            }
            
        }

    }
    
    
}