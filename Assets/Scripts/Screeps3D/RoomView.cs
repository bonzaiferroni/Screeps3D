using UnityEngine;

namespace Screeps3D {
    public class RoomView : MonoBehaviour {

        [SerializeField] private TerrainView terrain;
        [SerializeField] private EntityView entities;
        [SerializeField] private WorldView world;
        private WorldCoord coord;

        public void Load(WorldCoord coord) {
            this.coord = coord;
            terrain.Load(coord);
            entities.Load(coord);   
        }

        public void Target() {
            entities.Wake();
            world.LoadNeighbors(coord);
        }
    }
}