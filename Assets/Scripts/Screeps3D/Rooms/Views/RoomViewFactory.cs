using Common;
using UnityEngine;

namespace Screeps3D.Rooms.Views
{
    public class RoomViewFactory : BaseSingleton<RoomViewFactory>
    {
        [SerializeField] private GameObject _roomPrefab;

        public RoomView GenerateView(Room room)
        {
            var go = Instantiate(_roomPrefab);
            var view = go.GetComponent<RoomView>();
            view.transform.SetParent(transform);
            view.gameObject.name = room.Name;
            view.transform.localPosition = room.Position;
            view.Init(room);
            return view;
        }
    }
}