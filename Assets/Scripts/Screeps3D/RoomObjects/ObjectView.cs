using UnityEngine;
using Utils;

namespace Screeps3D {
    public class ObjectView : MonoBehaviour {
        
        internal RoomObject RoomObject { get; private set; }
        internal IScreepsComponent[] components;
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
                components = GetComponentsInChildren<IScreepsComponent>();
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
                vis.Hide();
            } else {
                gameObject.SetActive(false);   
            }
        }
    }

    internal interface IScreepsComponent {
        void Init(RoomObject roomObject);
        void Delta(JSONObject data);
    }
}