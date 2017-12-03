using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    internal class RoadView : MonoBehaviour
    {
        private int _x;
        private int _y;
        private MapView _mapView;
        private Dictionary<string, Renderer> _offshoots;

        public void Init(MapView mapView, int x, int y)
        {
            if (_offshoots == null)
            {
                _offshoots = new Dictionary<string, Renderer>();
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    _offshoots[renderer.gameObject.name] = renderer;
                }
            }

            this._x = x;
            this._y = y;
            this._mapView = mapView;
            CheckNeighbors();
        }

        private void CheckNeighbors()
        {
            var foundOffshoot = false;
            for (var xDelta = -1; xDelta <= 1; xDelta++)
            {
                for (var yDelta = -1; yDelta <= 1; yDelta++)
                {
                    var rx = _x + xDelta;
                    var ry = _y + yDelta;
                    if (xDelta == 0 && yDelta == 0)
                        continue;
                    if (_mapView.roads[rx, ry] == null)
                        continue;
                    var key = xDelta.ToString() + yDelta.ToString();
                    _offshoots[key].enabled = true;
                    foundOffshoot = true;
                }
            }
            if (!foundOffshoot)
            {
                _offshoots["base"].enabled = true;
            }
        }
    }
}