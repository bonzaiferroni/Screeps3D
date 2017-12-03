using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Screeps3D
{
    public class TerrainView : MonoBehaviour
    {
        [SerializeField] private PlaneDeformer _swamp;
        [SerializeField] private PlaneDeformer _wall;

        private bool[,] _wallPositions;
        private bool[,] _swampPositions;
        private int _x;
        private int _y;
        private string _terrain;

        public void Load(WorldCoord coord)
        {
            TerrainFinder.Instance.Find(coord, InitRender);
        }

        private void InitRender(string terrain)
        {
            this._terrain = terrain;
            _wallPositions = new bool[50, 50];
            _swampPositions = new bool[50, 50];
            _x = 0;
            _y = 0;
            enabled = true;
        }

        private void Update()
        {
            Render();
        }

        private void Render()
        {
            var time = Time.time;
            for (; _x < 50; _x++)
            {
                for (; _y < 50; _y++)
                {
                    var unit = _terrain[_x + _y * 50];
                    if (unit == '0' || unit == '1')
                    {
                    }
                    if (unit == '2' || unit == '3')
                    {
                        _swampPositions[_x, _y] = true;
                    }
                    if (unit == '1' || unit == '3')
                    {
                        _wallPositions[_x, _y] = true;
                    }
                    if (Time.time - time > .001f)
                    {
                        return;
                    }
                }
                _y = 0;
            }
            _wall.SetHeights(_wallPositions, .5f, .5f);
            _swamp.SetHeights(_swampPositions, .3f, 0);
            enabled = false;
        }
    }
}