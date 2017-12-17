using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class WallHeightView: MonoBehaviour, IObjectViewComponent
    {
        private static readonly Dictionary<int, float> Scales = new Dictionary<int, float>
        {
            {1, 0.3f},
            {1000000, 0.5f},
            {10000000, 1f},
            {100000000, 1.5f},
            {300000000, 2f}
        };

        private IHitpointsObject _wall;
        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _wall = roomObject as IHitpointsObject;
            SetScale();
        }

        public void Delta(JSONObject data)
        {
            if (data.HasField("hits"))
               SetScale();
            
        }

        private void SetScale()
        {
            if (_wall.Hits <= 1)
            {
                SetScaleY(0.3f);
                return;
            }

            if (_wall.Hits >= 290000000)
            {
                SetScaleY(2);
                return;
            }

            var heighest = Scales.First(h => h.Key >= _wall.Hits);
            var lowest = Scales.Last(h => h.Key < _wall.Hits);
            var factor = (_wall.Hits - lowest.Key) / (heighest.Key - lowest.Key);
            SetScaleY((heighest.Value - lowest.Value) * factor + lowest.Value);
        }

        private void SetScaleY(float height)
        {
            var ls = transform.localScale;
            transform.localScale = new Vector3(ls.x, height, ls.z);
        }

        public void Unload(RoomObject roomObject)
        {
        }
    }
}