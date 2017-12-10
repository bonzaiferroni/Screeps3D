using Screeps3D.Rooms;

namespace Screeps3D
{
    public class Flag : RoomObject, INamedObject
    {
        public string Name { get; set; }

        public int SecondaryColor { get; set; }
        public int PrimaryColor { get; set; }
        
        public Flag(string name)
        {
            Id = name;
            Name = name;
            Type = "flag";
        }

        public void FlagDelta(string[] dataArray, Room room)
        {
            PrimaryColor = int.Parse(dataArray[1]);
            SecondaryColor = int.Parse(dataArray[2]);
            X = int.Parse(dataArray[3]);
            Y = int.Parse(dataArray[4]);
            Room = room;
            RoomName = room.roomName;
        }
    }
}