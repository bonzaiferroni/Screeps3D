using System;
using UnityEngine;

namespace Screeps3D.Tools.Selection
{
    public abstract class Subpanel : MonoBehaviour
    {
        public abstract string Name { get; }
        public abstract Type ObjectType { get; }

        public virtual float Height
        {
            get { return rect.sizeDelta.y; }
        }

        public abstract void Load(RoomObject roomObject);
        public abstract void Unload();

        internal RectTransform rect;

        internal void Init()
        {
            rect = GetComponent<RectTransform>();
        }
    }
}