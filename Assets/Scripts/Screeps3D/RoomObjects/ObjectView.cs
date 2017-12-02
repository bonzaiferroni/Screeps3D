using UnityEngine;
using Utils;

namespace Screeps3D {
    [RequireComponent(typeof(ScaleVis))]
    public class ObjectView : MonoBehaviour {
        
        internal RoomObject RoomObject { get; private set; }
        internal IScreepsComponent[] components;
        private ScaleVis vis;

        public bool IsVisible { 
            get { return vis.IsVisible; }
        }

        internal virtual void Init(RoomObject roomObject) {
            if (components == null) {
                components = GetComponentsInChildren<IScreepsComponent>();
                vis = GetComponent<ScaleVis>();
                vis.OnFinishedAnimation += OnFinishedAnimation;
            }
            
            RoomObject = roomObject;
            transform.localPosition = new Vector3(RoomObject.X, transform.localPosition.y, 49 - RoomObject.Y);

            foreach (var component in components) {
                component.Init(roomObject);
            }
        }

        private void OnFinishedAnimation(bool isVisible) {
            if (!isVisible) {
                ObjectViewer.Instance.AddToPool(this);
            }
        }

        internal virtual void Delta(JSONObject data) {
            foreach (var component in components) {
                component.Delta(data);
            }
        }
        
        internal void Show() {
            vis.Show();
        }

        internal void Hide() {
            vis.Hide();
        }
    }

    internal interface IScreepsComponent {
        void Init(RoomObject roomObject);
        void Delta(JSONObject data);
    }
}