using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class WallHeightView: MonoBehaviour, IObjectViewComponent
    {
        private static readonly Dictionary<float, float> Heights = new Dictionary<float, float>
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
            SetHeight();
        }

        public void Delta(JSONObject data)
        {
            if (data.HasField("hits"))
               SetHeight();
            
        }

        private void SetHeight()
        {
            var heighest = Heights.First(h => h.Key > _wall.Hits);
            var lowest = Heights.Last(h => h.Key < _wall.Hits);
            var factor = (_wall.Hits - lowest.Key) / (heighest.Key - lowest.Key);
            var height = (heighest.Value - lowest.Value) * factor + lowest.Value;
            var ls = _gameObject.transform.localScale;
            _gameObject.transform.localScale = new Vector3(ls.x, height, ls.z);
        }

        public void Unload(RoomObject roomObject)
        {
        }
    }
}