namespace Screeps3D.RoomObjects
{
    /*{
        "_id":"594c4187c46642cc2ce46dff",
        "type":"nuker",
        "x":14,
        "y":18,
        "room":"W2S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "energy":300000,
        "energyCapacity":300000,
        "G":5000,
        "GCapacity":5000,
        "hits":1000,
        "hitsMax":1000,
        "cooldownTime":2.247301E+07
    }*/

    public class Nuker : Structure, IEnergyObject, IResourceObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }

        public float ResourceAmount { get; set; }
        public float ResourceCapacity { get; set; }
        public string ResourceType { get; set; }

        internal Nuker()
        {
            ResourceType = "G";
        }
        
        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);

            UnpackUtility.Energy(this, data);
            
            var minAmountData = data["G"];
            if (minAmountData != null)
            {
                ResourceAmount = minAmountData.n;
            }
            
            var minCapacityData = data["GCapacity"];
            if (minCapacityData != null)
            {
                ResourceCapacity = minCapacityData.n;
            }
        }
    }
}