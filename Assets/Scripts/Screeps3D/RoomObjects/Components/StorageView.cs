using UnityEngine;
using Utils.Utils;

namespace Screeps3D.Components
{
    public class StorageView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVisAxes energyDisplay;

        private Storage energyObject;

        public void Init(RoomObject roomObject)
        {
            energyObject = roomObject as Storage;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        private void AdjustScale()
        {
            energyDisplay.Visible(energyObject.Energy / energyObject.EnergyCapacity);
        }
    }
}