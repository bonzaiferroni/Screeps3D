using Common;
using TMPro;
using UnityEngine;

namespace Screeps3D
{
    public class Debugger : BaseSingleton<Debugger>
    {
        public static string ScreenText { get; set; }

        [SerializeField] private TextMeshProUGUI _label;

        private void Update()
        {
            _label.text = ScreenText;
        }
    }
}