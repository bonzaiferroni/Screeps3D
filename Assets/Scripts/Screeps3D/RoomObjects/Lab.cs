namespace Screeps3D
{
    /*{
        "_id":"594a260005d0cca1799cfc76",
        "type":"lab",
        "x":10,
        "y":19,
        "room":"W8S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "hits":500,
        "hitsMax":500,
        "mineralAmount":0,
        "cooldown":0,
        "mineralType":null,
        "mineralCapacity":3000,
        "energy":1080,
        "energyCapacity":2000,
        "actionLog":{
            "runReaction":null
        }
    }*/
    public class Lab : Structure, IEnergyObject
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