namespace Screeps3D {
    /*{
        "_id":"594a8f75895ce86149c2c013",
        "type":"terminal",
        "x":10,
        "y":21,
        "room":"W8S12",
        "notifyWhenAttacked":true,
        "user":"567d9401f60a26fc4c41bd38",
        "energy":30506,
        "energyCapacity":300000,
        "hits":3000,
        "hitsMax":3000,
        "L":5000,
        "K":5000,
        "XKHO2":10000,
        "power":5000,
        "H":10000,
        "XZH2O":10000,
        "XGH2O":10000,
        "O":5000,
        "X":5000,
        "send":null,
        "cooldownTime":2.246884E+07,
        "UH":30,
        "OH":5,
    }*/
    public class Terminal : Structure, IEnergyObject {
        public float Energy { get; private set; }
        public float EnergyCapacity { get; private set; }
        
        internal override void Unpack(JSONObject data) {
            base.Unpack(data);
            var energyObj = data["energy"];
            if (energyObj != null) {
                Energy = energyObj.n;
            }

            var energyCapacityObj = data["energyCapacity"];
            if (energyCapacityObj) {
                EnergyCapacity = energyCapacityObj.n;
            }
        }
    }
}