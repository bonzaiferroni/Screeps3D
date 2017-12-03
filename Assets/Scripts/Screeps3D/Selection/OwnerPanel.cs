using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Selection
{
    public class OwnerPanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Image _badge;

        private IOwnedObject _selected;
        private float _height;

        public override float Height { get { return _height; } }

        public override string Name
        {
            get { return "owner"; }
        }

        public override Type ObjectType
        {
            get { return typeof(IOwnedObject); }
        }

        public override void Load(RoomObject roomObject)
        {
            _selected = roomObject as IOwnedObject;
            if (_selected.Owner != null)
            {
                _height = rect.sizeDelta.y;
                gameObject.SetActive(true);
                _label.text = string.Format("Owner: {0}", _selected.Owner.username);
                _badge.sprite = Sprite.Create(_selected.Owner.badge,
                    new Rect(0.0f, 0.0f, BadgeManager.BADGE_SIZE, BadgeManager.BADGE_SIZE), new Vector2(.5f, .5f));
            } else
            {
                gameObject.SetActive(false);
                _height = 0;
            }
        }

        public override void Unload()
        {
            _selected = null;
        }
    }
}