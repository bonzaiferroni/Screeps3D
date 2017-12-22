using System;

namespace Common
{
    public interface IVisibilityMod
    {
        float CurrentVisibility { get; }
        float TargetVisibility { get; }
        bool IsVisible { get; }
        bool IsVisibleOnStart { get; }
        event Action<bool> OnFinishedAnimation;

        void SetVisibility(bool visible, bool instant = false);
        void SetVisibility(float visibility, bool instant = false);
        void Show(bool instant = false);
        void Hide(bool instant = false);
        void Toggle(bool instant = false);
    }
}
