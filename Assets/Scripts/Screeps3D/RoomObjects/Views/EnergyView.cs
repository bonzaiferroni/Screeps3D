using Common;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
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
            _energyDisplay.SetVisibility(_energyObject.Energy / _energyObject.EnergyCapacity);
        }
    }
}