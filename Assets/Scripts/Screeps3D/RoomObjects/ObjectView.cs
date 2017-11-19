using UnityEngine;
using Utils;

namespace Screeps3D {
    internal class ObjectView : MonoBehaviour {
        
        internal RoomObject RoomObject { get; private set; }
        internal IObjectComponent[] components;
        private ScaleVis vis;

        public bool IsVisible {
            get {
                if (vis) {
                    return vis.IsVisible;
                } else {
                    return gameObject.activeInHierarchy;
                }
            }
        }

        internal virtual void Init(RoomObject roomObject) {
            if (components == null) {
                components = GetComponentsInChildren<IObjectComponent>();
                vis = GetComponent<ScaleVis>();
            }
            
            RoomObject = roomObject;
            transform.localPosition = new Vector3(RoomObject.X, transform.localPosition.y, 49 - RoomObject.Y);

            foreach (var component in components) {
                component.Init(roomObject);
            }
        }

        internal virtual void Delta(JSONObject data) {
            foreach (var component in components) {
                component.Delta(data);
            }
        }
        
        internal void Show() {
            if (vis) {
                vis.Show();
            } else {
                gameObject.SetActive(true);   
            }
        }

        internal void Hide() {
            if (vis) {
                vis.Show();
            } else {
                gameObject.SetActive(false);   
            }
        }
    }

    internal interface IObjectComponent {
        void Init(RoomObject roomObject);
        void Delta(JSONObject data);
    }
}