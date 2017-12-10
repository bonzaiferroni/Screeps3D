using Common;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    [RequireComponent(typeof(ScaleVis))]
    public class ObjectView : MonoBehaviour
    {
        internal RoomObject RoomObject { get; private set; }
        internal IObjectViewComponent[] components;
        protected ScaleVis _vis;

        internal void Init()
        {
            components = GetComponentsInChildren<IObjectViewComponent>();
            _vis = GetComponent<ScaleVis>();
            _vis.OnFinishedAnimation += OnFinishedAnimation;

            foreach (var component in components)
            {
                component.Init();
            }
        }

        internal virtual void Load(RoomObject roomObject)
        {
            RoomObject = roomObject;
            transform.position = RoomObject.Position;
            roomObject.OnShow += Show;

            foreach (var component in components)
            {
                component.Load(roomObject);
            }

            Show(true);
        }

        internal virtual void Delta(JSONObject data)
        {
            foreach (var component in components)
            {
                component.Delta(data);
            }
        }

        private void OnFinishedAnimation(bool isVisible)
        {
            if (isVisible)
                return;

            foreach (var component in components)
            {
                component.Unload(RoomObject);
            }

            RoomObject.OnShow -= Show;
            RoomObject.DetachView();
            ObjectViewFactory.Instance.AddToPool(this);
            RoomObject = null;
        }

        internal void Show(bool shown)
        {
            if (shown)
            {
                _vis.Show();
            }
            else
            {
                _vis.Hide();
            }
        }
    }
}