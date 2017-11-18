using UnityEngine;

namespace Screeps3D {
    internal class ObjectView : MonoBehaviour {
        
        internal RoomObject RoomObject { get; private set; }
        internal IObjectComponent[] components;
        
        internal virtual void Init(RoomObject roomObject) {
            if (components == null) {
                components = GetComponentsInChildren<IObjectComponent>();
            }
            
            RoomObject = roomObject;
            transform.localPosition = new Vector3(RoomObject.X, transform.localPosition.y, 49 - RoomObject.Y);
            Show();

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
            gameObject.SetActive(true);
        }

        internal void Hide() {
            Destroy(gameObject);
        }
    }

    internal interface IObjectComponent {
        void Init(RoomObject roomObject);
        void Delta(JSONObject data);
    }
}