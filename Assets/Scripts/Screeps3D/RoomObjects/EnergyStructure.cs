namespace Screeps3D {
    public class EnergyStructure : Structure {
        public float Energy { get; private set; }
        public float EnergyCapacity { get; private set; }
        
        internal override void Unpack(JSONObject data) {
            base.Unpack(data);

            var energyCapacityObj = data["energyCapacity"];
            if (energyCapacityObj)
            {
                EnergyCapacity = energyCapacityObj.n;
            }

            var energyObj = data["energy"];
            if (energyObj != null)
            {
                Energy = energyObj.n > EnergyCapacity ? EnergyCapacity : energyObj.n;
            }
        }
    }
}