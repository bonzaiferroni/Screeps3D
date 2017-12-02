using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Screeps3D.Selection {
    
    [RequireComponent(typeof(VerticalLayoutGroup))]
    [RequireComponent(typeof(ScaleVis))]
    public class SelectionPanel : MonoBehaviour {

        public RoomObject Selected { get; private set; }
        public float Height { get; private set; }

        public Action<SelectionPanel> OnHidden;
        
        private float targetPos;
        private RectTransform rect;
        private bool inPosition;
        private float targetRef;
        private SelectionPanelComponent[] components;
        private VerticalLayoutGroup layoutGroup;
        private ScaleVis vis;

        private void Start() {
            vis.OnFinishedAnimation += OnFinishedAnimation;
        }

        private void OnFinishedAnimation(bool obj) {
            if (!obj) {
                OnHidden(this);
            }
        }

        public void Init() {
            rect = GetComponent<RectTransform>();
            components = GetComponentsInChildren<SelectionPanelComponent>();
            layoutGroup = GetComponent<VerticalLayoutGroup>();
            vis = GetComponent<ScaleVis>();
        }

        public void Load(RoomObject obj) {
            Selected = obj;
            vis.Show();
            rect.anchoredPosition = Vector2.zero;

            Height = 0;
            foreach (var component in components) {
                component.Load(obj);
                Height += component.Height;
            }
            rect.sizeDelta = new Vector2(0, Height);
        }

        public void Unload() {
            foreach (var component in components) {
                component.Unload();
            }
            
            Selected = null;
            vis.Hide();
        }

        public void SetPosition(float position) {
            inPosition = false;
            targetPos = -position;
        }

        private void Update() {
            if (Selected == null || inPosition)
                return;

            var anchor = rect.anchoredPosition;
            anchor.y = Mathf.SmoothDamp(anchor.y, targetPos, ref targetRef, .2f);
            rect.anchoredPosition = anchor;

            inPosition = Mathf.Abs(anchor.y - targetPos) < .001f;
        }
    }
}