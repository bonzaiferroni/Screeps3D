using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    internal class RoadView : MonoBehaviour
    {
        private int x;
        private int y;
        private MapView mapView;
        private Dictionary<string, Renderer> offshoots;

        public void Init(MapView mapView, int x, int y)
        {
            if (offshoots == null)
            {
                offshoots = new Dictionary<string, Renderer>();
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    offshoots[renderer.gameObject.name] = renderer;
                }
            }

            this.x = x;
            this.y = y;
            this.mapView = mapView;
            CheckNeighbors();
        }

        private void CheckNeighbors()
        {
            var foundOffshoot = false;
            for (var xDelta = -1; xDelta <= 1; xDelta++)
            {
                for (var yDelta = -1; yDelta <= 1; yDelta++)
                {
                    var rx = x + xDelta;
                    var ry = y + yDelta;
                    if (xDelta == 0 && yDelta == 0)
                        continue;
                    if (mapView.roads[rx, ry] == null)
                        continue;
                    var key = xDelta.ToString() + yDelta.ToString();
                    offshoots[key].enabled = true;
                    foundOffshoot = true;
                }
            }
            if (!foundOffshoot)
            {
                offshoots["base"].enabled = true;
            }
        }
    }
}