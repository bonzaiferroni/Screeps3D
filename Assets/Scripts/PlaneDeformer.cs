using UnityEngine;

namespace Screeps3D {
    public class PlaneDeformer : MonoBehaviour {
        private Mesh mesh;

        private void Start() {
            mesh = GetComponent<MeshFilter>().mesh;
        }
        
        public void Deform(Vector2[] points) {
        }
    }
}