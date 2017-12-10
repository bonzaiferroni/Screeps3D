using System.Collections.Generic;
using System.Linq;
using Common;
using Screeps3D.RoomObjects;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    public class SelectionUI : BaseSingleton<SelectionUI>
    {
        private const float PANEL_SPACING = 2;
        
        [SerializeField] private GameObject _panelPrefab;
        [SerializeField] private Transform _parent;

        private Stack<SelectionPanel> _pool = new Stack<SelectionPanel>();
        private List<SelectionPanel> _selections = new List<SelectionPanel>();
        private bool _selectionUpdated;
        private RectTransform _rect;

        private void Start()
        {
            Tools.Selection.Selection.Instance.OnSelect += OnSelect;
            Tools.Selection.Selection.Instance.OnDeselect += OnDeselect;
            _rect = _parent.GetComponent<RectTransform>();
        }

        private void OnSelect(RoomObject obj)
        {
            var panel = FindPanel(obj);
            if (panel != null)
                return;

            panel = NewPanel();
            panel.Load(obj);
            _selections.Add(panel);
            _selectionUpdated = true;
        }

        private void OnDeselect(RoomObject obj)
        {
            var panel = FindPanel(obj);
            if (panel == null)
                return;

            panel.Unload();
            _selections.Remove(panel);
            _selectionUpdated = true;
        }

        private SelectionPanel FindPanel(RoomObject obj)
        {
            return _selections.FirstOrDefault(panel => panel.Selected == obj);
        }

        private SelectionPanel NewPanel()
        {
            if (_pool.Count > 0)
                return _pool.Pop();

            var go = Instantiate(_panelPrefab);
            go.transform.SetParent(_parent, false);
            var panel = go.GetComponent<SelectionPanel>();
            panel.Init();
            panel.OnHidden += OnPanelHidden;
            return panel;
        }

        private void OnPanelHidden(SelectionPanel obj)
        {
            _pool.Push(obj);
        }

        private void Update()
        {
            if (!_selectionUpdated) return;
            SetPositions();
            _selectionUpdated = false;
        }

        private void SetPositions()
        {
            var height = 0f;
            foreach (var panel in _selections)
            {
                panel.SetPosition(height);
                height += panel.Height + PANEL_SPACING;
            }
            _rect.sizeDelta = new Vector3(_rect.sizeDelta.x, height);
        }
    }
}