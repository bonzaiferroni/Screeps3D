using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Utils;

namespace Screeps3D
{
    public class TerrainFinder : BaseSingleton<TerrainFinder>
    {
        [SerializeField] private ScreepsAPI api;

        private Dictionary<string, string> terrain = new Dictionary<string, string>();

        public void Find(WorldCoord coord, Action<string> callback)
        {
            if (terrain.ContainsKey(coord.key))
            {
                callback(terrain[coord.key]);
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
                this.terrain[coord.key] = terrainData;
                callback(terrainData);
            };

            api.Http.GetRoom(coord.roomName, coord.shardName, serverCallback);
        }
    }
}