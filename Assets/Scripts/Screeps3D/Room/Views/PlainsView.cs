using UnityEngine;

namespace Screeps3D
{
    public class PlainsView : MonoBehaviour, IRoomViewComponent
    {
        private Renderer _rend;
        private float _original;
        private float _current;
        private float _target;
        private float _targetRef;

        public void Init(Room room)
        {
            room.OnShowObjects += ManageHighlight;
        }

        private void ManageHighlight(bool showObjects)
        {
            if (showObjects)
            {
                Highlight();
            }
            else
            {
                Dim();
            }
        }

        public void Highlight()
        {
            if (!_rend)
            {
                _rend = GetComponent<Renderer>();
                _original = _rend.material.color.r;
            }
            _target = _original + .1f;
            enabled = true;
        }

        public void Dim()
        {
            _target = _original;
            enabled = true;
        }

        public void Update()
        {
            if (!_rend || Mathf.Abs(_current - _target) < .001f)
            {
                enabled = false;
                return;
            }
            _current = Mathf.SmoothDamp(_rend.material.color.r, _target, ref _targetRef, 1);
            _rend.material.color = new Color(_current, _current, _current);
        }
    }
}