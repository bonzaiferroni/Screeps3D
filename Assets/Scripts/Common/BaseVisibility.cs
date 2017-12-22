using System;
using UnityEngine;

namespace Common
{
    public abstract class BaseVisibility : MonoBehaviour, IVisibilityMod
    {
        public event Action<bool> OnFinishedAnimation;

        [SerializeField] private bool _animateOnStart = true;
        [SerializeField] private bool _visibleOnStart = true;
        [SerializeField] private float _speed = .2f;
        
        private float _targetRef;
        private bool _modified;
        
        public float CurrentVisibility { get; private set; }
        public float TargetVisibility { get; private set; }
        public bool IsVisible { get; private set; }
        public bool IsVisibleOnStart { get { return _visibleOnStart; }}

        private void Start()
        {
            if (_animateOnStart)
            {
                Modify(0);
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

        public virtual void SetVisibility(float target, bool instant = false)
        {
            _modified = true;

            if (target == TargetVisibility)
                return;
            
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
                Modify(TargetVisibility);
                enabled = false;
            }
            else
            {
                CurrentVisibility = Mathf.SmoothDamp(CurrentVisibility, TargetVisibility, ref _targetRef, _speed);
                Modify(CurrentVisibility);
            }
        }

        protected abstract void Modify(float amount);
    }
}