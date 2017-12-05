using UnityEngine;

namespace Screeps3D
{
    public class PosUtility
    {
        public static Vector3 Convert(int x, int y, Room room)
        {
            return room.position + new Vector3(x, 0, 49 - y);
        }
    }
}