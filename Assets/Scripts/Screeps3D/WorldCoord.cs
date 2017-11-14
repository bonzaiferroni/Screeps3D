using System;
using UnityEngine;

namespace Screeps3D {
    public class WorldCoord {
        public readonly string dirX;
        public readonly string dirY;
        public readonly int shard;
        public readonly int x;
        public readonly int y;
        public readonly Vector3 vector;

        public readonly string shardName;
        public readonly string roomName;
        public readonly string key;

        public WorldCoord(string dirX, string dirY, int shard, int x, int y) {
            this.dirX = dirX;
            this.dirY = dirY;
            this.shard = shard;
            this.x = x;
            this.y = y;
            vector = new Vector3(dirX == "E" ? x * 50 : (-x - 1) * 50, shard * 25,
                dirY == "N" ? y * 50 : (-y - 1) * 50);
            shardName = string.Format("shard{0}", shard);
            roomName = string.Format("{0}{1}{2}{3}", dirX, x, dirY, y);
            key = shardName + roomName;
        }

        public static WorldCoord Get(string roomName, string shardName) {
            string dirX, dirY;
            int x, y, index1, index2, shard;
            
            roomName = roomName.ToUpperInvariant();
            if (roomName.Contains("E")) {
                index1 = roomName.IndexOf("E", StringComparison.Ordinal);
                dirX = "E";
                Debug.Log("ey");
            } else if (roomName.Contains("W")) {
                index1 = roomName.IndexOf("W", StringComparison.Ordinal);
                dirX = "W";
                Debug.Log("ey");
            } else {
                return null;
            }
            
            if (roomName.Contains("N")) {
                index2 = roomName.IndexOf("N", StringComparison.Ordinal);
                dirY = "N";
                Debug.Log("ey");
            } else if (roomName.Contains("S")) {
                index2 = roomName.IndexOf("S", StringComparison.Ordinal);
                dirY = "S";
                Debug.Log("ey");
            } else {
                return null;
            }

            var parsed = int.TryParse(roomName.Substring(index1 + 1, index2 - index1 - 1), out x);
            if (!parsed)
                return null;
            parsed = int.TryParse(roomName.Substring(index2 + 1), out y);
            if (!parsed)
                return null;

            parsed = int.TryParse(shardName.Substring(5), out shard);
            if (!parsed)
                return null;

            return new WorldCoord(dirX, dirY, shard, x, y);
        }

        public WorldCoord Relative(int xDelta, int yDelta) {
            var dirX = this.dirX;
            var dirY = this.dirY;
            var x = this.x;
            var y = this.y;

            if (this.dirX == "E") {
                x += xDelta;
            } else {
                x -= xDelta;
            }
            if (x < 0) {
                dirX = this.dirX == "E" ? "W" : "E";
                x = -x - 1;
            }

            if (this.dirY == "N") {
                y += yDelta;
            } else {
                y -= yDelta;
            }
            if (y < 0) {
                dirY = this.dirX == "N" ? "S" : "N";
                y = -y - 1;
            }
            return new WorldCoord(dirX, dirY, shard, x, y);
        }
    }
}