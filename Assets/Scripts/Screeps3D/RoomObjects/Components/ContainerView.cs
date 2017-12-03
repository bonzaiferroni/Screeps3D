using UnityEngine;
using Utils;
using Utils.Utils;

namespace Screeps3D.RoomObjects.Components
{
    public class ContainerView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVisAxes energyDisplay;
        private Container container;

        public void Init(RoomObject roomObject)
        {
            container = roomObject as Container;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        private void AdjustScale()
        {
            energyDisplay.Visible(container.Energy / container.EnergyCapacity);
        }
    }
}