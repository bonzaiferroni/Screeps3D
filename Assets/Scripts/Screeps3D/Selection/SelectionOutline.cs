using System.Collections.Generic;
using cakeslice;
using UnityEngine;

namespace Screeps3D.Selection {
    public class SelectionOutline {

        private static ObjectView current;
        private static List<Outline> outlines = new List<Outline>();
        
        public static void DrawOutline(ObjectView view) {
            if (current) {
                if (view == current)
                    return;
                RemoveCurrent();
            }

            if (view) {
                AddCurrent(view);
            }
        }

        private static void RemoveCurrent() {
            foreach (var outline in outlines) {
                Object.Destroy(outline);
            }
            outlines.Clear();
            current = null;
        }

        private static void AddCurrent(ObjectView view) {
            foreach (var rend in view.GetComponentsInChildren<MeshRenderer>()) {
                var outline = rend.gameObject.AddComponent<Outline>();
                outlines.Add(outline);
            }
            current = view;
        }
    }
}