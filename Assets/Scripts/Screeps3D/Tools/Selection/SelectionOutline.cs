using System.Collections.Generic;
using cakeslice;
using Screeps3D.RoomObjects.Views;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    public class SelectionOutline
    {
        private static ObjectView _current;
        private static List<Outline> _outlines = new List<Outline>();

        public static void DrawOutline(ObjectView view)
        {
            if (_current)
            {
                if (view == _current)
                    return;
                RemoveCurrent();
            }

            if (view)
            {
                AddCurrent(view);
            }
        }

        private static void RemoveCurrent()
        {
            foreach (var outline in _outlines)
            {
                Object.Destroy(outline);
            }
            _outlines.Clear();
            _current = null;
        }

        private static void AddCurrent(ObjectView view)
        {
            foreach (var rend in view.GetComponentsInChildren<MeshRenderer>())
            {
                var outline = rend.gameObject.AddComponent<Outline>();
                _outlines.Add(outline);
            }
            _current = view;
        }
    }
}