using UnityEngine;
using Utils;

namespace Screeps3D
{
    [RequireComponent(typeof(ScaleVis))]
    public class ObjectView : MonoBehaviour
    {
        internal RoomObject RoomObject { get; private set; }
        internal IScreepsComponent[] components;
        private ScaleVis _vis;

        public bool IsVisible
        {
            get { return _vis.IsVisible; }
        }

        internal virtual void Init(RoomObject roomObject)
        {
            if (components == null)
            {
                components = GetComponentsInChildren<IScreepsComponent>();
                _vis = GetComponent<ScaleVis>();
                _vis.OnFinishedAnimation += OnFinishedAnimation;
            }

            RoomObject = roomObject;
            transform.localPosition = new Vector3(RoomObject.X, transform.localPosition.y, 49 - RoomObject.Y);

            foreach (var component in components)
            {
                component.Init(roomObject);
            }
        }

        private void OnFinishedAnimation(bool isVisible)
        {
            if (!isVisible)
            {
                ObjectViewer.Instance.AddToPool(this);
            }
        }

        internal virtual void Delta(JSONObject data)
        {
            foreach (var component in components)
            {
                component.Delta(data);
            }
        }

        internal void Show()
        {
            _vis.Show();
        }

        internal void Hide()
        {
            _vis.Hide();
        }
    }

    internal interface IScreepsComponent
    {
        void Init(RoomObject roomObject);
        void Delta(JSONObject data);
    }
}