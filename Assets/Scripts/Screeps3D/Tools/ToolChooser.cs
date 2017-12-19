using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Tools
{
    public class ToolChooser : BaseSingleton<ToolChooser>
    {
        [SerializeField] private Toggle _selectionToggle;
        [SerializeField] private Toggle _flagToggle;
        [SerializeField] private Toggle _constructionToggle;
        
        public ToolType CurrentTool { get; private set; }
        public Action<ToolType> OnToolChange;

        private void Start()
        {
            _selectionToggle.onValueChanged.AddListener(isOn => ToggleInput(isOn, ToolType.Selection));
            _flagToggle.onValueChanged.AddListener(isOn => ToggleInput(isOn, ToolType.Flag));
            _constructionToggle.onValueChanged.AddListener(isOn => ToggleInput(isOn, ToolType.Construction));
        }

        private void ToggleInput(bool isOn, ToolType toolType)
        {
            if (!isOn)
                return;
            CurrentTool = toolType;
            if (OnToolChange != null)
                OnToolChange(toolType);
        }
    }

    public enum ToolType
    {
        Selection,
        Flag,
        Construction
    }
}