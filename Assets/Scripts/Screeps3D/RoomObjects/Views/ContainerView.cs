using UnityEngine;
using Utils;
using Utils.Utils;

namespace Screeps3D.RoomObjects.Components
{
    public class ContainerView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;
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
            _energyDisplay.Visible(_container.Energy / _container.EnergyCapacity);
        }
    }
}