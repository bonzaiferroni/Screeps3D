namespace Screeps3D
{
    public class UnpackUtility
    {
        internal static void Energy(IEnergyObject energyObj, JSONObject data)
        {
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

        internal static void Name(INamedObject obj, JSONObject data)
        {
            var nameData = data["name"];
            if (nameData != null)
            {
                obj.Name = nameData.str;
            }
        }

        internal static void HitPoints(IHitpointsObject obj, JSONObject data)
        {
            var hitsData = data["hits"];
            if (hitsData != null)
            {
                obj.Hits = hitsData.n;
            }

            var hitsMaxData = data["hitsMax"];
            if (hitsMaxData != null)
            {
                obj.HitsMax = hitsMaxData.n;
            }
        }
    }

    internal interface IEnergyObject
    {
        float Energy { get; set; }
        float EnergyCapacity { get; set; }
    }

    internal interface INamedObject
    {
        string Name { get; set; }
    }

    internal interface IHitpointsObject
    {
        float Hits { get; set; }
        float HitsMax { get; set; }
    }
}