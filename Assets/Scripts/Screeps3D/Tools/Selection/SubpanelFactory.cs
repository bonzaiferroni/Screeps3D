using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    public class SubpanelFactory : BaseSingleton<SubpanelFactory>
    {
        [SerializeField] private GameObject[] _panelPrefabs;
        [SerializeField] private Transform _inactiveParent;

        private Dictionary<string, Stack<Subpanel>> _pools = new Dictionary<string, Stack<Subpanel>>();
        private List<Func<RoomObject, Subpanel>> _factories = new List<Func<RoomObject, Subpanel>>();

        private void Start()
        {
            for (var i = 0; i < _panelPrefabs.Length; i++)
            {
                var panel = _panelPrefabs[i];
                var component = panel.GetComponent<Subpanel>();
                if (!component) continue;
                _pools[component.Name] = new Stack<Subpanel>();

                _factories.Add(roomObject =>
                {
                    if (!component.ObjectType.IsInstanceOfType(roomObject))
                    {
                        return null;
                    }
                    if (_pools[component.Name].Count > 0)
                    {
                        return _pools[component.Name].Pop();
                    } else
                    {
                        var go = Instantiate(panel.gameObject);
                        var subpanel = go.GetComponent<Subpanel>();
                        subpanel.Init();
                        return subpanel;
                    }
                });
            }
        }

        internal void AddSubpanels(SelectionPanel parent)
        {
            // remove previous
            foreach (var subpanel in parent.subpanels)
            {
                _pools[subpanel.Name].Push(subpanel);
                subpanel.transform.SetParent(_inactiveParent, false);
                subpanel.transform.localScale = Vector3.one;
            }

            parent.subpanels.Clear();
            parent.Height = 0f;
            foreach (var factory in _factories)
            {
                var panel = factory(parent.Selected);
                if (!panel) continue;
                parent.subpanels.Add(panel);
                panel.Load(parent.Selected);
                panel.transform.SetParent(parent.transform, false);
                panel.rect.anchoredPosition = new Vector2(0, -parent.Height);
                parent.Height += panel.Height;
            }
            parent.Height += 10;
            parent.rect.sizeDelta = new Vector2(0, parent.Height);
        }
    }
}