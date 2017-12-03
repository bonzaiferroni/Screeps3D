using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public class PlaneDeformer : MonoBehaviour
    {
        private MeshFilter _filter;
        private bool[,] _positions;
        private float _constant;
        private float _random;
        private int _i;

        public void SetHeights(bool[,] positions, float constant, float random)
        {
            _filter = GetComponent<MeshFilter>();
            enabled = true;
            this._positions = positions;
            this._constant = constant;
            this._random = random;
            _i = 0;
        }

        private void Update()
        {
            Deform();
        }

        public void Deform()
        {
            var time = Time.time;
            var vertices = _filter.mesh.vertices;
            for (; _i < vertices.Length; _i++)
            {
                var point = vertices[_i];
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
                    vertices[_i] = new Vector3(point.x, _constant + Random.value * _random, point.z);
                }
                if (Time.time - time > .001f)
                {
                    _filter.mesh.vertices = vertices;
                    return;
                }
            }

            _filter.mesh.vertices = vertices;
            _filter.mesh.RecalculateNormals();
            enabled = false;
        }
    }
}