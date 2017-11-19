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

        private bool[,] wallPositions;
        private bool[,] swampPositions;
        private int x;
        private int y;
        private string terrain;

        public void Load(WorldCoord coord) {
            finder.Find(coord, InitRender);
        }

        private void InitRender(string terrain) {
            this.terrain = terrain;
            wallPositions = new bool[50, 50];
            swampPositions = new bool[50, 50];
            x = 0;
            y = 0;
            enabled = true;
        }

        private void Update() {
            Render();
        }

        private void Render() {
            var time = Time.time;
            for (; x < 50; x++) {
                for (; y < 50; y++) {
                    var unit = terrain[x + y * 50];
                    if (unit == '0' || unit == '1') {
                    }
                    if (unit == '2' || unit == '3') {
                        swampPositions[x, y] = true;
                    }
                    if (unit == '1' || unit == '3') {
                        wallPositions[x, y] = true;
                    }
                    if (Time.time - time > .001f) {
                        return;
                    } 
                }
                y = 0;
            }
            wall.SetHeights(wallPositions, .5f, .5f);
            swamp.SetHeights(swampPositions, .3f, 0);
            enabled = false;
        }
    }
}