using System;
using Common;
using Screeps3D.Rooms;
using UnityEngine;

namespace Screeps3D.Player
{
    public class PlayerPosition : BaseSingleton<PlayerPosition>
    {
        private int _xPos;
        private int _yPos;
        
        public int ShardLevel { get; private set; }
        public Room Room { get; private set; }
        public string ShardName { get; private set; }
        public string RoomName { get; private set; }
        public int YCoord { get; private set; }
        public int XCoord { get; private set; }
        public string YDir { get; private set; }
        public string XDir { get; private set; }

        public Action OnRoomChange;

        private void Update()
        {
            var xPos = (int) Mathf.Floor(transform.position.x / 50);
            var yPos = (int) Mathf.Floor(transform.position.z / 50);
            var shardLevel = (int) (transform.position.y / Constants.ShardHeight);
            if (_xPos == xPos && _yPos == yPos && shardLevel == ShardLevel)
                return;
            
            _xPos = xPos;
            _yPos = yPos;
            ShardLevel = shardLevel;

            XDir = _xPos >= 0 ? "E" : "W";
            YDir = _yPos >= 0 ? "N" : "S";
            XCoord = _xPos >= 0 ? _xPos : -_xPos - 1;
            YCoord = _yPos >= 0 ? _yPos : -_yPos - 1;
            
            RoomName = string.Format("{0}{1}{2}{3}", XDir, XCoord, YDir, YCoord);
            ShardName = string.Format("shard{0}", ShardLevel);
            Room = RoomManager.Instance.Get(RoomName, ShardName);

            if (OnRoomChange != null)
                OnRoomChange();
        }
    }
}