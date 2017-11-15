using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D {
    public class EntityView : MonoBehaviour {
        
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private EntityFactory factory;
        
        private Dictionary<string, ScreepsObject> objects = new Dictionary<string, ScreepsObject>();
        
        private JSONObject currentData;
        private WorldCoord coord;

        public void Load(WorldCoord coord) {
            this.coord = coord;

            if (api.Address.hostName.ToLowerInvariant() == "screeps.com") {
                api.Socket.Subscribe(string.Format("room:{0}/{1}", coord.shardName, coord.roomName), OnRoomData);
            } else {
                api.Socket.Subscribe(string.Format("room:{0}", coord.roomName), OnRoomData);
            }
        }

        private void OnDestroy() {
            if (api.Socket != null && coord != null) {
                if (api.Address.hostName.ToLowerInvariant() == "screeps.com") {
                    api.Socket.Unsub(string.Format("room:{0}/{1}", coord.shardName, coord.roomName));
                } else {
                    api.Socket.Unsub(string.Format("room:{0}", coord.roomName));
                }
            }
        }

        private void OnRoomData(JSONObject data) {
            currentData = data;
        }

        private void Update() {
            if (currentData != null) {
                RenderEntities();
                currentData = null;
            }
        }

        private void RenderEntities() {
            var objects = currentData["objects"];
            foreach (var id in objects.keys) {
                var obj = objects[id];
                if (!this.objects.ContainsKey(id)) {
                    var newSo = factory.Get(id, obj);
                    if (newSo == null) 
                        continue;
                    newSo.transform.SetParent(transform, false);
                    newSo.gameObject.SetActive(true);
                    newSo.LoadObject(obj);
                    this.objects[id] = newSo;
                }
                var so = this.objects[id];
                so.UpdateObject(obj);
            }
        }
    }
}