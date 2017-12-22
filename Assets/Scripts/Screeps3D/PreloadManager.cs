using Common;
using Screeps3D.Rooms.Views;
using UnityEngine;

namespace Screeps3D
{
    public class PreloadManager : MonoBehaviour
    {
        private void Start()
        {
            PoolLoader.Preload(MapDotView.Path, 1000);
        }
    }
}