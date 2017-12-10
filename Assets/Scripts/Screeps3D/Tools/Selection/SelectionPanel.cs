using System;
using System.Collections.Generic;
using Common;
using Screeps3D.RoomObjects;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    [RequireComponent(typeof(ScaleVis))]
    public class SelectionPanel : MonoBehaviour
    {
        public RoomObject Selected { get; private set; }
        public float Height { get; internal set; }

        public Action<SelectionPanel> OnHidden;

        internal List<Subpanel> subpanels = new List<Subpanel>();
        internal RectTransform rect;

        private float _targetPos;
        private bool _inPosition;
        private float _targetRef;
        private ScaleVis _vis;

        private void Start()
        {
            _vis.OnFinishedAnimation += OnFinishedAnimation;
        }

        private void OnFinishedAnimation(bool visible)
        {
            if (!visible)
            {
                OnHidden(this);
            }
        }

        public void Init()
        {
            rect = GetComponent<RectTransform>();
            _vis = GetComponent<ScaleVis>();
        }

        public void Load(RoomObject obj)
        {
            Selected = obj;
            _vis.Show();
            rect.anchoredPosition = Vector2.zero;

            SubpanelFactory.Instance.AddSubpanels(this);
        }

        public void Unload()
        {
            foreach (var component in subpanels)
            {
                component.Unload();
            }

            Selected = null;
            _vis.Hide();
        }

        public void SetPosition(float position)
        {
            _inPosition = false;
            if (rect.anchoredPosition.y > -position)
            {
                rect.anchoredPosition = new Vector2(0, -position);
            }
            _targetPos = -position;
        }

        private void Update()
        {
            if (Selected == null || _inPosition)
                return;

            var anchor = rect.anchoredPosition;
            anchor.y = Mathf.SmoothDamp(anchor.y, _targetPos, ref _targetRef, .2f);
            rect.anchoredPosition = anchor;

            _inPosition = Mathf.Abs(anchor.y - _targetPos) < .001f;
        }
    }
}