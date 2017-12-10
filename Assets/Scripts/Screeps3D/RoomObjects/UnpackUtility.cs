using Screeps_API;

namespace Screeps3D.RoomObjects
{
    public class UnpackUtility
    {
        
        internal static void Id(RoomObject roomObject, JSONObject data)
        {
            var idObj = data["_id"];
            if (idObj != null)
                roomObject.Id = idObj.str;
        }
        
        internal static void Type(RoomObject roomObject, JSONObject data)
        {
            var typeObj = data["type"];
            if (typeObj != null)
                roomObject.Type = typeObj.str;
        }
        
        internal static void Position(RoomObject roomObject, JSONObject data)
        {
            var xObj = data["x"];
            if (xObj != null)
                roomObject.X = (int) xObj.n;

            var yObj = data["y"];
            if (yObj != null)
                roomObject.Y = (int) yObj.n;

            var roomNameObj = data["room"];
            if (roomNameObj != null)
                roomObject.RoomName = roomNameObj.str;
        }
        
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

        internal static void Owner(IOwnedObject obj, JSONObject data)
        {
            var userData = data["user"];
            if (userData != null)
            {
                obj.UserId = userData.str;
                obj.Owner = ScreepsAPI.Instance.UserManager.GetUser(userData.str); 
            }
        }

        internal static void Decay(IDecay obj, JSONObject data)
        {
            var decayData = data["nextDecayTime"];
            if (decayData != null)
            {
                obj.NextDecayTime = decayData.n;
            }
        }

        internal static void Progress(IProgress progressObj, JSONObject data)
        {
            var progressData = data["progress"];
            if (progressData != null)
            {
                progressObj.Progress = progressData.n;
            }
        }
    }
}