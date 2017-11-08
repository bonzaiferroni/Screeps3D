using UnityEngine;

namespace Screeps3D {
    public class RoomView : MonoBehaviour {

        [SerializeField] private TerrainView terrain;
        [SerializeField] private EntityView entities;
        
        public void Load(WorldCoord coord, bool renderEntities) {
            terrain.Load(coord);
            if (renderEntities) {
                entities.Load(coord);   
            }
        }
    }
}