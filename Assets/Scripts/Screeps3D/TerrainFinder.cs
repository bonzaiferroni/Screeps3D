using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Screeps3D {
    public class TerrainFinder : MonoBehaviour {
        
        [SerializeField] private ScreepsAPI api;
        
        private Dictionary<string, string> terrain = new Dictionary<string, string>();

        public void Find(WorldCoord coord, Action<string> callback) {
            if (terrain.ContainsKey(coord.key)) {
                callback(terrain[coord.key]);
                return;
            }

            Action<JSONObject> serverCallback = obj => {
                var terrain = obj["terrain"].list[0]["terrain"].str;
                this.terrain[coord.key] = terrain;
                callback(terrain);
            };
            
            api.Http.GetRoom(coord.roomName, coord.shardName, serverCallback);
        }
    }
}