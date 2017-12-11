using System;
using System.Linq;
using System.Text;
using Screeps3D.RoomObjects;
using TMPro;
using UnityEngine;

namespace Screeps3D.Tools.Selection.Subpanels
{
    public class StorePanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;

        private IStoreObject _selected;
        private RoomObject _roomObject;

        public override string Name
        {
            get { return "store"; }
        }

        public override Type ObjectType
        {
            get { return typeof(IStoreObject); }
        }

        public override void Load(RoomObject roomObject)
        {
            _roomObject = roomObject;
            _roomObject.OnDelta += OnDelta;
            _selected = roomObject as IStoreObject;
            UpdateLabel();
        }
        
        private void UpdateLabel()
        {
            var resources = _selected.Store
                .Where(a => a.Value > 0)
                .OrderBy(a => GetResourceOrder(a.Key)).ToList();

            var t = transform as RectTransform;
            t.sizeDelta = new Vector2(t.sizeDelta.x, resources.Count * 20);
            
            var sb = new StringBuilder();
            foreach (var resource in resources)
                sb.AppendLine(string.Format("{0}: {1}", char.ToUpper(resource.Key[0]) + resource.Key.Substring(1), resource.Value));

            _label.text = sb.ToString();
        }

        private void OnDelta(JSONObject obj)
        {
            var hasChanged = obj.keys.Any(k => Constants.RESOURCES_ALL.Contains(k));
            
            if (hasChanged)
                UpdateLabel();
        }
    
        private static short GetResourceOrder(string resourceType)
        {
            switch (resourceType[0])
            {
                case 'e':
                    return 0;
                case 'p':
                    return 1;
                default:
                    return (short) resourceType.Length;
            }
        }

        public override void Unload()
        {
            _roomObject.OnDelta -= OnDelta;
            _roomObject = null;
            _selected = null;
        }
    }
}