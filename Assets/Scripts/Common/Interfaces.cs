namespace Utils
{
    public interface IVisibilityControl
    {
        bool IsVisible { get; }
        void Visible(bool shown, bool instant = false);
        void Visible(float target, bool instant = false);
        void Show(bool instant = false);
        void Hide(bool instant = false);
        void Toggle(bool instant = false);
    }
}