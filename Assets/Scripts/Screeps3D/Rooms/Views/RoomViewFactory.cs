using Common;
using UnityEngine;

namespace Screeps3D.Rooms.Views
{
    public static class RoomViewFactory
    {
        private static Transform _parent;
        private const string Path = "Prefabs/RoomView";
        
        public static RoomView GetInstance(Room room)
        {
            if (_parent == null)
            {
                _parent = new GameObject("RoomViews").transform;
            }
            
            var go = PrefabLoader.Load(Path, _parent);
            var view = go.GetComponent<RoomView>();
            view.gameObject.name = room.Name;
            view.transform.localPosition = room.Position;
            view.Init(room);
            return view;
        }
    }
}