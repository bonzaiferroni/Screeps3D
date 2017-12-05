using System;

namespace Screeps3D
{
    public class RoomFactory
    {
        public Room Generate(string roomName, string shardName)
        {
            string xDir, yDir;
            int xCoord, yCoord, index1, index2, shard;

            roomName = roomName.ToUpperInvariant();
            if (roomName.Contains("E"))
            {
                index1 = roomName.IndexOf("E", StringComparison.Ordinal);
                xDir = "E";
            } else if (roomName.Contains("W"))
            {
                index1 = roomName.IndexOf("W", StringComparison.Ordinal);
                xDir = "W";
            } else
            {
                return null;
            }

            if (roomName.Contains("N"))
            {
                index2 = roomName.IndexOf("N", StringComparison.Ordinal);
                yDir = "N";
            } else if (roomName.Contains("S"))
            {
                index2 = roomName.IndexOf("S", StringComparison.Ordinal);
                yDir = "S";
            } else
            {
                return null;
            }

            var parsed = int.TryParse(roomName.Substring(index1 + 1, index2 - index1 - 1), out xCoord);
            if (!parsed)
                return null;
            parsed = int.TryParse(roomName.Substring(index2 + 1), out yCoord);
            if (!parsed)
                return null;

            parsed = int.TryParse(shardName.Substring(5), out shard);
            if (!parsed)
                return null;

            return new Room(roomName, shardName, xDir, yDir, shard, xCoord, yCoord);
        }
    }
}