using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class WallHeightView: MonoBehaviour, IObjectViewComponent
    {
        private static readonly Dictionary<int, float> Scales = new Dictionary<int, float>
        {
            {1, 0.01f},
            {1000000, 0.1f},
            {10000000, 1},
            {100000000, 1.5f},
            {300000000, 2f}
        };

        private IHitpointsObject _wall;
        private GameObject _gameObject;
        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _gameObject = roomObject.View.gameObject;
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
            if (_wall.Hits > 290000000)
            {
                SetHeight(2);
                return;
            }
            var heighest = Scales.First(h => h.Key >= _wall.Hits);
            var lowest = Scales.Last(h => h.Key <= _wall.Hits);
            var factor = (_wall.Hits - lowest.Key) / (heighest.Key - lowest.Key);
            SetHeight((heighest.Value - lowest.Value) * factor + lowest.Value);
        }

        private void SetHeight(float height)
        {
            var ls = _gameObject.transform.localScale;
            _gameObject.transform.localScale = new Vector3(ls.x, height, ls.z);
        }

        public void Unload(RoomObject roomObject)
        {
        }
    }
}