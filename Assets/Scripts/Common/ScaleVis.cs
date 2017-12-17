using System;
using Screeps3D.RoomObjects.Views;
using UnityEngine;

namespace Common
{
    public class ScaleVis : MonoBehaviour, IVisibilityMod
    {
        [SerializeField] private bool _animateOnStart = true;
        [SerializeField] private bool _visibleOnStart = true;
        [SerializeField] private float _speed = .2f;

        public bool IsVisible { get; private set; }

        public Action<bool> OnFinishedAnimation
        {
            get { return _onFinishedAnimation; }
            set { _onFinishedAnimation = value; }
        }

        public float CurrentVisibility { get; private set; }
        public float TargetVisibility { get; private set; }
        private float _targetRef;
        private Action<bool> _onFinishedAnimation;
        private bool _modified;

        private void Start()
        {
            if (_animateOnStart)
            {
                Scale(0);
            }
            if (!_modified)
            {
                SetVisibility(_visibleOnStart, !_animateOnStart);
            }
        }

        public void SetVisibility(bool show, bool instant = false)
        {
            var target = show ? 1 : 0;
            SetVisibility(target, instant);
        }

        public void SetVisibility(float target, bool instant = false)
        {
            _modified = true;
            enabled = true;
            IsVisible = target > 0;

            if (float.IsNaN(target) || target < 0)
            {
                target = 0;
            }
            
            if (target > 1)
            {
                target = 1;
            }

            this.TargetVisibility = target;

            if (instant)
            {
                CurrentVisibility = target;
            }
        }

        public void Show(bool instant = false)
        {
            SetVisibility(true, instant);
        }

        public void Hide(bool instant = false)
        {
            SetVisibility(false, instant);
        }

        public void Toggle(bool instant = false)
        {
            SetVisibility(!IsVisible, instant);
        }

        public void Update()
        {
            if (Mathf.Abs(CurrentVisibility - TargetVisibility) < .0001f)
            {
                if (OnFinishedAnimation != null)
                    OnFinishedAnimation(IsVisible);
                Scale(TargetVisibility);
                enabled = false;
            }
            else
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, _speed);
                Scale(CurrentVisibility);
            }
        }

        protected virtual void Scale(float amount)
        {
            transform.localScale = Vector3.one * amount;
        }
    }
}