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

    public class Nuker : Structure, IEnergyObject
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