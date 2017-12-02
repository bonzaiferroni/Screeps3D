using UnityEngine;

namespace Screeps3D.Selection {
    public abstract class SelectionPanelComponent : MonoBehaviour {
        public abstract float Height { get; }
        public abstract void Load(RoomObject roomObject);
        public abstract void Unload();
    }
}