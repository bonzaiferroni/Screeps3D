using Common;
using Screeps_API;
using UnityEngine;

namespace Screeps3D.Tools
{
    public class SidePanel : MonoBehaviour
    {
        [SerializeField] private FadePanel _panel;
        
        private void Start()
        {
            PanelManager.OnModeChange += OnModeChange;
            _panel.Hide(true);
        }

        private void OnModeChange(PanelMode mode)
        {
            _panel.SetVisibility(mode == PanelMode.Room || mode == PanelMode.Map);
        }
    }
}