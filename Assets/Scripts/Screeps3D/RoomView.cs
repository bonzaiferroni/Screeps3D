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
        private WorldCoord _coord;
        private bool _loadedNeighbors;

        public void Load(WorldCoord coord)
        {
            this._coord = coord;
            _terrain.Load(coord);
            _entities.Load(coord);
            _map.Load(coord);
        }

        public void Target()
        {
            _entities.Wake();
            _map.Wake();
            if (!_loadedNeighbors)
            {
                _loadedNeighbors = true;
                WorldView.Instance.LoadNeighbors(_coord);
            }
        }

        public void Show()
        {
            _vis.Show();
        }
    }
}