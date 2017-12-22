using Common;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class ContainerView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private ScaleAxes _energyDisplay;
        private Container _container;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _container = roomObject as Container;
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
            _energyDisplay.SetVisibility(_container.TotalResources / _container.StoreCapacity);
        }
    }
}