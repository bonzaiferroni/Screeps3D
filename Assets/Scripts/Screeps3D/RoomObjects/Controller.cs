namespace Screeps3D
{
    /*{
         public int DowngradeTime { get; private set; }
        public int Level { get; private set; }
        public int Progress { get; private set; }
        public int SafeModeTicksLeft { get; private set; }
        public string UserId { get; private set; }
        "safeMode": 20257,
        "safeModeAvailable": 2
    }*/

    public class Controller : Structure
    {
        public int Level { get; private set; }

        internal override void Unpack(JSONObject data)
        {
            base.Unpack(data);

            var levelObj = data["level"];
            if (levelObj != null)
            {
                Level = (int) levelObj.n;
            }
        }
    }
}