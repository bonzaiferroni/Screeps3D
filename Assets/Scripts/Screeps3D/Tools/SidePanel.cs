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
            GameManager.OnModeChange += OnModeChange;
            _panel.Hide(true);
        }

        private void OnModeChange(GameMode mode)
        {
            _panel.SetVisibility(mode == GameMode.Room || mode == GameMode.Map);
        }
    }
}