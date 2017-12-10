using System.Collections.Generic;
using Common;

namespace Screeps3D.Rooms
{
    public class RoomManager : BaseSingleton<RoomManager>
    {
        
        private RoomFactory _factory = new RoomFactory();
        private Dictionary<string, Room> _rooms = new Dictionary<string,Room>();

        public Room Get(string roomName, string shardName)
        {
            var key = roomName + shardName;
            if (_rooms.ContainsKey(key))
            {
                return _rooms[key];
            }

            var room = _factory.Generate(roomName, shardName);
            if (room == null)
                return null;

            _rooms[key] = room;
            return room;
        }

        public Room GetNeighbor(Room origin, int xDelta, int yDelta)
        {
            var xDir = origin.XDir;
            var yDir = origin.YDir;
            var xCoord = origin.XCoord;
            var yCoord = origin.YCoord;

            if (origin.XDir == "E")
            {
                xCoord += xDelta;
            } else
            {
                xCoord -= xDelta;
            }
            if (xCoord < 0)
            {
                xDir = origin.XDir == "E" ? "W" : "E";
                xCoord = -xCoord - 1;
            }

            if (origin.YDir == "N")
            {
                yCoord += yDelta;
            } else
            {
                yCoord -= yDelta;
            }
            if (yCoord < 0)
            {
                yDir = origin.YDir == "N" ? "S" : "N";
                yCoord = -yCoord - 1;
            }

            var roomName = string.Format("{0}{1}{2}{3}", xDir, xCoord, yDir, yCoord);
            var shardName = string.Format("shard{0}", origin.ShardNumber);
            
            return Get(roomName, shardName);
        }
    }
}