using System;
using System.Collections.Generic;
using Common;
using Screeps3D.RoomObjects;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    [RequireComponent(typeof(ScaleVis))]
    public class SelectionPanel : VerticalPanelElement
    {
        private List<SelectionSubpanel> _subpanels = new List<SelectionSubpanel>();
        
        [SerializeField] private VerticalPanelGroup _panelGroup;
        
        private bool _inPosition;
        private float _targetRef;
        
        private void Start()
        {
            Vis.OnFinishedAnimation += OnFinishedAnimation;
        }

        public void Init()
        {
            Vis = GetComponent<ScaleVis>();
        }

        public void Load(RoomObject obj)
        {
            Vis.Show();

            SubpanelFactory.Instance.AddSubpanels(obj, _subpanels);

            foreach (var subpanel in _subpanels)
            {
                _panelGroup.AddElement(subpanel);
            }
        }

        public void Unload()
        {
            foreach (var subpanel in _subpanels)
            {
                subpanel.Unload();
            }

            Vis.Hide();
        }

        private void OnFinishedAnimation(bool visible)
        {
            if (!visible)
            {
                ReturnSubpanels();
                PoolLoader.Return(Path, gameObject);
            }
        }

        private void ReturnSubpanels()
        {
            SubpanelFactory.Instance.ReturnSubpanels(_subpanels);
            
            _panelGroup.ClearElements();
        }
        
        // POOLED OBJECT
        
        private const string Path = "Prefabs/Selection/selectionPanel";

        public static SelectionPanel GetInstance()
        {
            var panel = PoolLoader.Load(Path).GetComponent<SelectionPanel>();
            panel.Init();
            return panel;
        }
    }
}