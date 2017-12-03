using UnityEngine;
using Utils.Utils;

namespace Screeps3D.Components
{
    public class TerminalView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;
        private Terminal _terminal;

        public void Init(RoomObject roomObject)
        {
            _terminal = roomObject as Terminal;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        private void AdjustScale()
        {
            _energyDisplay.Visible(_terminal.Energy / _terminal.EnergyCapacity);
        }
    }
}