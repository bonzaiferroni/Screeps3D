using UnityEngine;
using Utils;

namespace Screeps3D {
    public class RoomView : MonoBehaviour {

        [SerializeField] private TerrainView terrain;
        [SerializeField] private EntityView entities;
        [SerializeField] private WorldView world;
        [SerializeField] private ScaleVis vis;
        private WorldCoord coord;
        private bool loadedNeighbors;

        public void Load(WorldCoord coord) {
            this.coord = coord;
            terrain.Load(coord);
            entities.Load(coord);   
        }

        public void Target() {
            entities.Wake();
            if (!loadedNeighbors) {
                loadedNeighbors = true;
                world.LoadNeighbors(coord);
            }
        }

        public void Show() {
            vis.Show();
        }
    }
}