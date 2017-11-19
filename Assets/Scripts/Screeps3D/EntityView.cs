using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D {
    public class EntityView : MonoBehaviour {
        
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private ObjectManager manager;
        [SerializeField] private PlainsView plains;
        
        private Dictionary<string, RoomObject> roomObjects = new Dictionary<string, RoomObject>();
        private Queue<JSONObject> roomData = new Queue<JSONObject>();
        private WorldCoord coord;
        private string path;
        private bool awake;
        
        private static Queue<EntityView> queue = new Queue<EntityView>();

        public void Load(WorldCoord coord) {
            this.coord = coord;
            
            if (api.Address.hostName.ToLowerInvariant() == "screeps.com") {
                path = string.Format("room:{0}/{1}", coord.shardName, coord.roomName);
            } else {
                path = string.Format("room:{0}", coord.roomName);
            }
        }

        public void Wake() {
            if (awake)
                return;

            if (queue.Count >= 2) {
                var otherView = queue.Dequeue();
                otherView.Sleep();
            }
            queue.Enqueue(this);
            
            Debug.Log("subscribing: " + path);
            api.Socket.Subscribe(path, OnRoomData);
            plains.Highlight();
            awake = true;
        }

        private void Sleep() {
            api.Socket.Unsub(path);
            plains.Dim();
            awake = false;
        }

        private void OnDestroy() {
            if (api.Socket != null && coord != null) {
                api.Socket.Unsub(path);
            }
        }

        private void OnRoomData(JSONObject data) {
            roomData.Enqueue(data);
        }

        private void Update() {
            if (roomData.Count == 0)
                return;
            RenderEntities(roomData.Dequeue());
        }

        private void RenderEntities(JSONObject data) {
            UnpackBadges(data);
            var objects = data["objects"];
            foreach (var id in objects.keys) {
                var datum = objects[id];
                
                if (datum["type"] && datum["type"].str == "structureWall") {
                    Debug.Log(datum);
                }

                RoomObject roomObject;
                if (roomObjects.ContainsKey(id)) {
                    roomObject = roomObjects[id];
                } else {
                    roomObject = manager.Get(id, datum, this);
                    roomObjects[id] = roomObject;
                }
                
                if (datum.IsNull) {
                    manager.Remove(id);
                    roomObjects.Remove(id);
                } else {
                    roomObject.Delta(datum);   
                }
            }
        }

        private void UnpackBadges(JSONObject data) {
            var userObj = data["users"];
            if (userObj == null) {
                return;
            }

            foreach (var id in userObj.keys) {
                var datum = userObj[id];
                api.Badges.CacheBadge(id, datum);
            } 
        }
    }
}