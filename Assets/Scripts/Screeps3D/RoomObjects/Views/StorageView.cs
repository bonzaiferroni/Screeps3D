using Common.Utils;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class StorageView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;

        private Storage _energyObject;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _energyObject = roomObject as Storage;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        public void Unload(RoomObject roomObject)
        {
        }

        private void AdjustScale()
        {
            _energyDisplay.Visible(_energyObject.TotalResources / _energyObject.StoreCapacity);
        }
    }
}