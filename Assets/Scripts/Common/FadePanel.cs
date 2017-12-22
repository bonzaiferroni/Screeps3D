using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadePanel : BaseVisibility
    {
        private CanvasGroup _cgroup;

        protected virtual void Awake()
        {
            _cgroup = GetComponent<CanvasGroup>();
        }

        protected override void Modify(float amount)
        {
            _cgroup.alpha = amount;
        }

        public override void SetVisibility(float target, bool instant = false)
        {
            base.SetVisibility(target, instant);
            _cgroup.blocksRaycasts = IsVisible;
        }
    }
}