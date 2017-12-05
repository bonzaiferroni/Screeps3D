using UnityEngine;

namespace Screeps3D
{
    public class Room
    {
        public readonly string name;
        public readonly string roomName;
        public readonly string shardName;
        public readonly string xDir;
        public readonly string yDir;
        public readonly int shardNumber;
        public readonly int xCoord;
        public readonly int yCoord;
        public readonly Vector3 position;

        public RoomView View { get; set; }

        public Room(string roomName, string shardName, string xDir, string yDir, int shardNumber, int xCoord, int yCoord)
        {
            name = roomName + shardName;
            this.roomName = roomName;
            this.shardName = shardName;
            this.xDir = xDir;
            this.yDir = yDir;
            this.shardNumber = shardNumber;
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            position = new Vector3(xDir == "E" ? xCoord * 50 : (-xCoord - 1) * 50, shardNumber * 25,
                yDir == "N" ? yCoord * 50 : (-yCoord - 1) * 50);
        }

        public void Init(RoomView view)
        {
            View = view;
        }
    }
}