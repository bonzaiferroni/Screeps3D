using System;
using System.Collections.Generic;
using Common;
using Screeps3D.RoomObjects;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    public class SubpanelFactory : BaseSingleton<SubpanelFactory>
    {
        [SerializeField] private Transform _inactiveParent;

        private Dictionary<string, Stack<Subpanel>> _pools = new Dictionary<string, Stack<Subpanel>>();
        private List<Func<RoomObject, Subpanel>> _factories = new List<Func<RoomObject, Subpanel>>();
        private string _path = "Prefabs/Selection/Subpanels/";

        private string[] _prefabNames =
        {
            "Type", "Owner", "Name", "Pos", "Hits", "Energy", "Age", "Fatigue", "Decay", "Progress", "Construction", 
            "Capacity", "Store", "Cooldown", "Resource"
        };
        
        private void Start()
        {
            for (var i = 0; i < _prefabNames.Length; i++)
            {
                var panelName = _prefabNames[i];
                var panel = PrefabLoader.Look(string.Format("{0}{1}", _path, panelName));
                if (!panel)
                    continue;
                
                var component = panel.GetComponent<Subpanel>();
                if (!component) 
                    continue;
                
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