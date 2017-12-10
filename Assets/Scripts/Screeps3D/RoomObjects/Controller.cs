using Screeps_API;

namespace Screeps3D.RoomObjects
{
    /*{
      "_id": "5a0c8318ab17fd00012bf03d",
      "type": "controller",
      "room": "E1S2",
      "x": 24,
      "y": 16,
      "level": 8,
      "user": "5a0da017ab17fd00012bf0e7",
      "progress": 0,
      "downgradeTime": 1315168,
      "reservation":{"user":"5a0da017ab17fd00012bf0e7","endTime":1168667}
      "safeModeAvailable": 7
    }*/

    public class Controller : Structure, IOwnedObject, IProgress
    {
        public int Level { get; private set; }
        public string UserId { get; set; }
        public ScreepsUser Owner { get; set; }
        public float Progress { get; set; }
        public float ProgressMax { get; set; }
        public float DowngradeTime { get; set; }
        public ScreepsUser ReservedBy { get; set; }
        public float ReservationEnd { get; set; }
        public float SafeModesAvailable { get; set; }

        internal override void Unpack(JSONObject data, bool initial)
        {
            base.Unpack(data, initial);
            UnpackUtility.Owner(this, data);

            var levelObj = data["level"];
            if (levelObj != null)
            {
                Level = (int) levelObj.n;
            }

            UnpackUtility.Progress(this, data);
            ProgressMax = 0;
            if (Constants.CONTROLLER_LEVELS.ContainsKey(Level))
            {
                ProgressMax = Constants.CONTROLLER_LEVELS[Level];
            }

            var downgradeTimeData = data["downgradeTime"];
            if (downgradeTimeData != null)
            {
                DowngradeTime = downgradeTimeData.n;
            }

            var reservationData = data["reservation"];
            if (reservationData != null)
            {
                if (reservationData.IsNull)
                {
                    ReservedBy = null;
                } else
                {
                    var userData = reservationData["user"];
                    if (userData != null)
                    {
                        ReservedBy = ScreepsAPI.Instance.UserManager.GetUser(userData.str);
                    }

                    var endTimeData = reservationData["endTime"];
                    if (endTimeData != null)
                    {
                        ReservationEnd = endTimeData.n;
                    }
                }
            }

            var safeModeAvailableData = data["safeModeAvailable"];
            if (safeModeAvailableData != null)
            {
                SafeModesAvailable = safeModeAvailableData.n;
            }
        }
    }
}