using UnityEngine;

namespace Screeps3D {
    public static class Constants {
        public const string TYPE_STORAGE = "storage";
        public const string TYPE_EXTENSION = "extension";
        public const string TYPE_SPAWN = "spawn";
        public const string TYPE_CREEP = "creep";
        public const string TYPE_TOWER = "tower";

        public static Color RANGED_ATTACK_COLOR;

        static Constants() {
            ColorUtility.TryParseHtmlString("#4a708b", out RANGED_ATTACK_COLOR);
            //#8470ff
        }
    }
}