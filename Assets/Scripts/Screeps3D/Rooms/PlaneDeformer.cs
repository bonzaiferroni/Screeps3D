using Common;
using UnityEngine;

namespace Screeps3D.Rooms
{
    public class PlaneDeformer : MonoBehaviour
    {
        private MeshFilter _filter;
        private bool[,] _positions;
        private float _constant;
        private float _random;

        public void SetHeights(bool[,] positions, float constant, float random)
        {
            _filter = GetComponent<MeshFilter>();
            _positions = positions;
            _constant = constant;
            _random = random;
            Scheduler.Instance.Add(Deform);
        }

        public void Deform()
        {
            var vertices = _filter.mesh.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                var point = vertices[i];
                if (point.x < 0 || point.x > 50 || point.z < 0 || point.z > 50)
                    continue;

                var x = (int) point.x;
                if (x >= _positions.GetLength(0))
                {
                    continue;
                }
                var y = 49 - (int) point.z;
                if (y >= _positions.GetLength(1))
                {
                    continue;
                }
                if (_positions[x, y])
                {
                    vertices[i] = new Vector3(point.x, _constant + Random.value * _random, point.z);
                }
            }

            _filter.mesh.vertices = vertices;
            _filter.mesh.RecalculateNormals();
        }
    }
}