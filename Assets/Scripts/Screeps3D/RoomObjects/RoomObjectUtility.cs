namespace Screeps3D.RoomObjects
{
    public class RoomObjectUtility
    {
        internal static void UnpackEnergy(JSONObject data, IEnergyObject obj)
        {
            var energyCapacityObj = data["energyCapacity"];
            if (energyCapacityObj)
            {
                obj.EnergyCapacity = energyCapacityObj.n;
            }

            var energyObj = data["energy"];
            if (energyObj != null)
            {
                obj.Energy = energyObj.n > obj.EnergyCapacity ? obj.EnergyCapacity : energyObj.n;
            }
        }
    }
}