using Common;
using UnityEngine;
using Utils;

namespace Screeps3D
{
    public class RoomView : MonoBehaviour
    {
        [SerializeField] private TerrainView _terrain;
        [SerializeField] private EntityView _entities;
        [SerializeField] private MapView _map;
        [SerializeField] private ScaleVis _vis;
        private Room _room;
        private bool _loadedNeighbors;

        public void Target()
        {
            _entities.Wake();
            _map.Wake();
            if (!_loadedNeighbors)
            {
                _loadedNeighbors = true;
                WorldView.Instance.LoadNeighbors(_room);
            }
        }

        public void Show()
        {
            _vis.Show();
        }

        public void Init(Room room)
        {
            _room = room;
            Scheduler.Instance.Add(() => _terrain.Load(room));
            Scheduler.Instance.Add(() => _entities.Load(room));
            Scheduler.Instance.Add(() => _map.Load(room));
        }
    }
}