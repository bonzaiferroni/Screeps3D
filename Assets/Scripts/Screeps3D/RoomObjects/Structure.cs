namespace Screeps3D
{
    public class Structure : RoomObject, IHitpointsObject
    {
        public float Hits { get; set; }
        public float HitsMax { get; set; }

        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);

            UnpackUtility.HitPoints(this, data);
        }
    }
}