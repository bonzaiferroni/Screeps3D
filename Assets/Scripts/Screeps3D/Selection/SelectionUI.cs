using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace Screeps3D.Selection {
    public class SelectionUI : BaseSingleton<SelectionUI> {

        [SerializeField] private GameObject panelPrefab;
        [SerializeField] private Transform parent;
        
        private Stack<SelectionPanel> pool = new Stack<SelectionPanel>();
        private List<SelectionPanel> selections = new List<SelectionPanel>();
        private bool selectionUpdated;
        private RectTransform rect;

        private void Start() {
            Selection.Instance.OnSelect += OnSelect;
            Selection.Instance.OnDeselect += OnDeselect;
            rect = parent.GetComponent<RectTransform>();
        }

        private void OnSelect(RoomObject obj) {
            var panel = FindPanel(obj);
            if (panel != null)
                return;
            
            panel = NewPanel();
            panel.Load(obj);
            selections.Add(panel);
            selectionUpdated = true;
        }

        private void OnDeselect(RoomObject obj) {
            var panel = FindPanel(obj);
            if (panel == null)
                return;

            panel.Unload();
            selections.Remove(panel);
            selectionUpdated = true;
        }

        private SelectionPanel FindPanel(RoomObject obj) {
            return selections.FirstOrDefault(panel => panel.Selected == obj);
        }

        private SelectionPanel NewPanel() {
            if (pool.Count > 0)
                return pool.Pop();

            var go = Instantiate(panelPrefab);
            go.transform.SetParent(parent, false);
            var panel = go.GetComponent<SelectionPanel>();
            panel.Init();
            panel.OnHidden += OnPanelHidden;
            return panel;
        }

        private void OnPanelHidden(SelectionPanel obj) {
            pool.Push(obj);
        }

        private void Update() {
            if (!selectionUpdated) return;
            SetPositions();
            selectionUpdated = false;
        }

        private void SetPositions() {
            var height = 0f;
            foreach (var panel in selections) {
                panel.SetPosition(height);
                height += panel.Height;
            }
            rect.sizeDelta = new Vector3(rect.sizeDelta.x, height);
        }
    }
}