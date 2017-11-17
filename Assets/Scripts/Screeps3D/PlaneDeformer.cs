using UnityEngine;

namespace Screeps3D {
    public class PlaneDeformer : MonoBehaviour {
        [SerializeField] private MeshFilter filter;
        
        public void Deform(Vector2[] points) {
        }

        public void SetHeights(bool[,] positions, int i) {
            foreach (var point in filter.mesh.vertices) {
                Debug.Log(point);
            }
        }
    }
}