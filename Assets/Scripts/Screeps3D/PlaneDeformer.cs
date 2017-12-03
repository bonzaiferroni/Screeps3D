using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public class PlaneDeformer : MonoBehaviour
    {
        private MeshFilter filter;
        private bool[,] positions;
        private float constant;
        private float random;
        private int i;

        public void SetHeights(bool[,] positions, float constant, float random)
        {
            filter = GetComponent<MeshFilter>();
            enabled = true;
            this.positions = positions;
            this.constant = constant;
            this.random = random;
            i = 0;
        }

        private void Update()
        {
            Deform();
        }

        public void Deform()
        {
            var time = Time.time;
            var vertices = filter.mesh.vertices;
            for (; i < vertices.Length; i++)
            {
                var point = vertices[i];
                if (point.x < 0 || point.x > 50 || point.z < 0 || point.z > 50)
                    continue;

                var x = (int) point.x;
                if (x >= positions.GetLength(0))
                {
                    continue;
                }
                var y = 49 - (int) point.z;
                if (y >= positions.GetLength(1))
                {
                    continue;
                }
                if (positions[x, y])
                {
                    vertices[i] = new Vector3(point.x, constant + Random.value * random, point.z);
                }
                if (Time.time - time > .001f)
                {
                    filter.mesh.vertices = vertices;
                    return;
                }
            }

            filter.mesh.vertices = vertices;
            filter.mesh.RecalculateNormals();
            enabled = false;
        }
    }
}