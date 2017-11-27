using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Screeps3D {
    [DisallowMultipleComponent]
    public class ObjectManager : MonoBehaviour
    {
        private static ObjectManager _instance;
        public static ObjectManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
                return _instance;
            }
        }

        public Dictionary<string, RoomObject> Cache { get; private set; }
        
        private Dictionary<string, ObjectView> prototypes = new Dictionary<string, ObjectView>();
        private Dictionary<string, ObjectView> viewCache = new Dictionary<string, ObjectView>();
        private ObjectFactory factory = new ObjectFactory();

        public List<ObjectView> GetViews()
        {
            return viewCache.Values.ToList();
        }

        private void Awake()
        {
            if (Instance != null && Instance.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                Destroy(this);
        }

        private void Start() {

            Cache = new Dictionary<string, RoomObject>();
            
            for (var i = 0; i < transform.childCount; i++) {
                var prototype = transform.GetChild(i).gameObject.GetComponent<ObjectView>();
                if (prototype == null || !prototype.gameObject.activeInHierarchy) continue; 
                prototypes[prototype.name] = prototype;
                prototype.gameObject.SetActive(false);
            }
        }

        internal RoomObject GetInstance(string id, JSONObject data, EntityView entityView) {
            if (Cache.ContainsKey(id)) {
                var existingRoomObject = Cache[id]; 
                var existingView = existingRoomObject.View;
                if (existingView != null) {
                    existingView.Show();
                    existingView.transform.SetParent(entityView.transform, false);
                }
                existingRoomObject.Init(data, existingView);
                
                return Cache[id];
            }

            var type = data["type"].str;
            
            var view = GetView(id, type);
            if (view != null) {
                view.transform.SetParent(entityView.transform, false);   
            }
            
            var roomObject = factory.Get(type);
            roomObject.Init(data, view);
            Cache[id] = roomObject;
            
            return roomObject;
        }
        
        private ObjectView GetView(string id, string type) {
            if (viewCache.ContainsKey(id)) 
                return viewCache[id];
            if (!prototypes.ContainsKey(type))
                return null;
            
            var so = Instantiate(prototypes[type].gameObject).GetComponent<ObjectView>();
            so.gameObject.SetActive(true);
            viewCache[id] = so;
            return so;
        }

        public void Remove(string id) {
            if (!Cache.ContainsKey(id)) {
                return;
            }
            var roomObject = Cache[id];
            if (roomObject.View) {
                roomObject.View.Hide();   
            }
        }
    }
}
