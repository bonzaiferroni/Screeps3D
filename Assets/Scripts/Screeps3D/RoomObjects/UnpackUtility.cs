namespace Screeps3D {
    public class UnpackUtility {
        internal static void Energy(IEnergyObject energyObj, JSONObject data) {
            var energyCapData = data["energyCapacity"];
            if (energyCapData)
            {
                energyObj.EnergyCapacity = energyCapData.n;
            }

            var energyData = data["energy"];
            if (energyData != null)
            {
                energyObj.Energy = energyData.n > energyObj.EnergyCapacity ? energyObj.EnergyCapacity : energyData.n;
            }
        }
    }
}