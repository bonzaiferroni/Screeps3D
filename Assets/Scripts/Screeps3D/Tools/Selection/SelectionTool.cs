using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    public class SelectionTool : MonoBehaviour
    {
        private void Start()
        {
            ToolChooser.Instance.OnToolChange += OnToolChange;
        }
        
        private void OnToolChange(ToolType toolType)
        {
            var activated = toolType == ToolType.Selection;
            Selection.Instance.enabled = activated;
        }
    }
}