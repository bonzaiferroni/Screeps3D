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
            ScreepsAPI.Instance.OnConnectionStatusChange += OnConnectionStatusChange;
            _panel.Show(false, true);
        }

        private void OnConnectionStatusChange(bool isConnected)
        {
            _panel.Show(isConnected);
        }
    }
}