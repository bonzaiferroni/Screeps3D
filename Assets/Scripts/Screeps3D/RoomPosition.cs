using UnityEngine;

namespace Screeps3D {
    public class RoomPosition {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string RoomName { get; private set; }

        public RoomPosition(int x, int y, string roomName) {
            X = x;
            Y = y;
            RoomName = roomName;
        }

        public void Delta(JSONObject data) {
            var xObj = data["x"];
            if (xObj != null) {
                X = (int) xObj.n;
            }
            var yObj = data["y"];
            if (yObj != null) {
                Y = (int) yObj.n;
            }
        }
    }
}