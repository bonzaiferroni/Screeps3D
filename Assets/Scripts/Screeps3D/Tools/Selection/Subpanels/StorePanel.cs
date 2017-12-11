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
            _selected = roomObject as IStoreObject;
            roomObject.OnDelta += OnDelta;
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
            var hasChanged = Constants.RESOURCES_ALL.Any(obj.HasField);
            
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
            _selected = null;
        }
    }
}