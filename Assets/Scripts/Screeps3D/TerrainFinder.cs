using System;
using System.Collections.Generic;
using Common;
using Screeps3D.Rooms;
using Screeps_API;
using UnityEngine;

namespace Screeps3D
{
    public class TerrainFinder : BaseSingleton<TerrainFinder>
    {
        private Dictionary<Room, string> _terrain = new Dictionary<Room, string>();

        public void Find(Room room, Action<string> callback)
        {
            if (_terrain.ContainsKey(room))
            {
                callback(_terrain[room]);
                return;
            }

            Action<string> serverCallback = str =>
            {
                var obj = new JSONObject(str);
                var errorObj = obj["error"];
                string terrainData;
                if (errorObj != null)
                {
                    terrainData = new string('1', 2500);
                } else
                {
                    terrainData = obj["terrain"].list[0]["terrain"].str;
                }
                this._terrain[room] = terrainData;
                callback(terrainData);
            };

            ScreepsAPI.Http.GetRoom(room.RoomName, room.ShardName, serverCallback);
        }
    }
}