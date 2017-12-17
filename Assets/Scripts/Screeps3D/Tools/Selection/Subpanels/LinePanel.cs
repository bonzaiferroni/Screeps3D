namespace Screeps3D.Tools.Selection.Subpanels
{
    public abstract class LinePanel : SelectionSubpanel
    {
        public override float Height { get { return Rect.sizeDelta.y; } }
    }
}