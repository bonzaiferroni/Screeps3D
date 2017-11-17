using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Screeps3D {
    public class TerrainView : MonoBehaviour {

        [SerializeField] private PlaneDeformer swamp;
        [SerializeField] private PlaneDeformer wall;
        [SerializeField] private TerrainFinder finder;
        // [SerializeField] private TerrainFactory factory;

        public void Load(WorldCoord coord) {
            finder.Find(coord, RenderRoom);
        }

        private void RenderRoom(string terrain) {
            var wallPositions = new bool[50, 50];
            var swampPositions = new bool[50, 50];
            for (var x = 0; x < 50; x++) {
                for (var y = 0; y < 50; y++) {
                    var unit = terrain[x + y * 50];
                    if (unit == '0' || unit == '1') {
                        // RenderTerrain(x, y, TerrainType.Plains);
                    }
                    if (unit == '2' || unit == '3') {
                        // RenderTerrain(x, y, TerrainType.Swamp);
                        swampPositions[x, y] = true;
                    }
                    if (unit == '1' || unit == '3') {
                        // RenderTerrain(x, y, TerrainType.Wall);
                        wallPositions[x, y] = true;
                    }
                }
            }
            wall.SetHeights(wallPositions, 1);
        }

        /*private void RenderTerrain(int x, int y, TerrainType type) {
            var go = factory.Get(type);
            go.transform.SetParent(transform, false);
            go.transform.localPosition = new Vector3(x, go.transform.localPosition.y, 49 - y);
            go.SetActive(true);
        }*/
    }

    public enum TerrainType {
        Plains,
        Wall,
        Swamp,
    }
}