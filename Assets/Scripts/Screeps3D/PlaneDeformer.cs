using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D {
    public class PlaneDeformer : MonoBehaviour {
        [SerializeField] private MeshFilter filter;

        public void SetHeights(bool[,] positions, float constant, float random) {
            var vertices = filter.mesh.vertices;
            for (var i = 0; i < vertices.Length; i++) {
                var point = vertices[i];
                if (point.x < 0 || point.x > 50 || point.z < 0 || point.z > 50)
                    continue;
                
                var x = (int) point.x;
                if (x >= positions.GetLength(0)) {
                    continue;
                }
                var y = 49 - (int) point.z;
                if (y >= positions.GetLength(1)) {
                    continue;
                }
                if (positions[x, y]) {
                    vertices[i] = new Vector3(point.x, constant + Random.value * random, point.z);
                }
            }
            filter.mesh.vertices = vertices;
            filter.mesh.RecalculateNormals();
        }
    }
}