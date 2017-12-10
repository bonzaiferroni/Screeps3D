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
            view.gameObject.name = room.name;
            view.transform.localPosition = room.position;
            view.Init(room);
            return view;
        }
    }
}