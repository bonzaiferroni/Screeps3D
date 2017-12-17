using System;

namespace Common
{
    public interface IVisibilityMod
    {
        float CurrentVisibility { get; }
        float TargetVisibility { get; }
        bool IsVisible { get; }
        Action<bool> OnFinishedAnimation { get; set; }

        void SetVisibility(bool visible, bool instant = false);
        void SetVisibility(float visibility, bool instant = false);
        void Show(bool instant = false);
        void Hide(bool instant = false);
        void Toggle(bool instant = false);
    }
}
