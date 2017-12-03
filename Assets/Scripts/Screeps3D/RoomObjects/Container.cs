namespace Screeps3D
{
    /*{
      "_id": "5a0f1cadcd842f00014a8007",
      "type": "container",
      "x": 16,
      "y": 38,
      "room": "E2S7",
      "notifyWhenAttacked": true,
      "energy": 62,
      "energyCapacity": 2000,
      "hits": 225000,
      "hitsMax": 250000,
      "nextDecayTime": 8858
    }*/

    public class Container : Structure, IEnergyObject
    {
        public float Energy { get; set; }
        public float EnergyCapacity { get; set; }

        internal override void Unpack(JSONObject data)
        {
            base.Unpack(data);

            UnpackUtility.Energy(this, data);
        }
    }
}