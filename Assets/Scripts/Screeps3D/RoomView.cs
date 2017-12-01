using UnityEngine;
using Utils;

namespace Screeps3D {
    public class RoomView : MonoBehaviour {

        [SerializeField] private TerrainView terrain;
        [SerializeField] private EntityView entities;
        [SerializeField] private MapView map;
        [SerializeField] private ScaleVis vis;
        private WorldCoord coord;
        private bool loadedNeighbors;

        public void Load(WorldCoord coord) {
            this.coord = coord;
            terrain.Load(coord);
            entities.Load(coord);
            map.Load(coord);
        }

        public void Target() {
            entities.Wake();
            map.Wake();
            if (!loadedNeighbors) {
                loadedNeighbors = true;
                WorldView.Instance.LoadNeighbors(coord);
            }
        }

        public void Show() {
            vis.Show();
        }
    }
}