using System;
using UnityEngine;

namespace Common
{
    public class VerticalPanelElement : MonoBehaviour
    {
        
        public VerticalPanelGroup Group { get; internal set; }

        [SerializeField] protected IVisibilityMod Vis;
        
        private float _height;
        private float _targetPos;
        private RectTransform _rect;
        private float _posRef;

        public virtual float Height
        {
            get { return _height; }
            set
            {
                if (_height == value)
                    return;
                _height = value;
                if (Group)
                    Group.UpdateGeometry();
            }
        }

        public float TargetPos
        {
            get { return _targetPos; }
            set
            {
                _targetPos = value;
                if (Vis != null && Vis.CurrentVisibility > .01f)
                {
                    enabled = true;
                }
                else
                {
                    SetPos(value);
                }
            }
        }

        public RectTransform Rect
        {
            get
            {
                if (!_rect)
                    _rect = GetComponent<RectTransform>();
                return _rect;
            }
        }

        private void Update()
        {
            if (Mathf.Abs(Rect.anchoredPosition.y - TargetPos) < .0001f)
            {
                SetPos(TargetPos);
                enabled = false;
            }
            else
            {
                SetPos(Mathf.SmoothDamp(Rect.anchoredPosition.y, TargetPos, ref _posRef, .2f));
            }
        }

        private void SetPos(float value)
        {
            Rect.anchoredPosition = Vector2.up * value;
        }
    }
}