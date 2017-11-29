using UnityEngine;
using Utils.Utils;

namespace Screeps3D.Components {
    public class TerminalView : MonoBehaviour, IScreepsComponent {
        [SerializeField] private ScaleVisAxes energyDisplay;
        private Terminal terminal;

        public void Init(RoomObject roomObject) {
            terminal = roomObject as Terminal;
            AdjustScale();
        }

        public void Delta(JSONObject data) {
            AdjustScale();
        }

        private void AdjustScale() {
            energyDisplay.Visible(terminal.Energy / terminal.EnergyCapacity);
        }
        
    }
}