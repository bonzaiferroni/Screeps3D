using UnityEngine;
using Utils;

namespace Screeps3D.Components
{
    public class EnergyView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private ScaleVis _energyDisplay;

        private IEnergyObject _energyObject;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _energyObject = roomObject as IEnergyObject;
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
            _energyDisplay.Visible(_energyObject.Energy / _energyObject.EnergyCapacity);
        }
    }
}