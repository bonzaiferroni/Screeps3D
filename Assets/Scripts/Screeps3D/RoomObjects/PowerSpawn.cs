namespace Screeps3D.RoomObjects
{
    /*{
        "_id":"594fd4a759b455ab3f6aa147",
        "type":"powerSpawn",
        "x":8,
        "y":21,
        "room":"W8S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "power":69,
        "powerCapacity":100,
        "energy":4450,
        "energyCapacity":5000,
        "hits":5000,
        "hitsMax":5000
    }*/
    public class PowerSpawn : Structure, IEnergyObject, IResourceObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }
        public float ResourceAmount { get; set; }
        public float ResourceCapacity { get; set; }
        public string ResourceType { get; set; }

        internal PowerSpawn()
        {
            ResourceType = "power";
        }
        
        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);

            UnpackUtility.Energy(this, data);

            var minAmountData = data["power"];
            if (minAmountData != null)
            {
                ResourceAmount = minAmountData.n;
            }
            
            var minCapacityData = data["powerCapacity"];
            if (minCapacityData != null)
            {
                ResourceCapacity = minCapacityData.n;
            }
        }
    }
}