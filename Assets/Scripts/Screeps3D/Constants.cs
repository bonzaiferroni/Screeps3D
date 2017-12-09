using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public static class Constants
    {
        public const string TYPE_STORAGE = "storage";
        public const string TYPE_EXTENSION = "extension";
        public const string TYPE_SPAWN = "spawn";
        public const string TYPE_CREEP = "creep";
        public const string TYPE_TOWER = "tower";
        public const string TYPE_CONTROLLER = "controller";
        public const string TYPE_TERMINAL = "terminal";
        public const string TYPE_CONTAINER = "container";
        public const string TYPE_LINK = "link";
        public const string TYPE_RAMPART = "rampart";

        public static readonly Dictionary<int, float> CONTROLLER_LEVELS = new Dictionary<int, float>
        {
            {1, 200},
            {2, 45000},
            {3, 135000},
            {4, 405000},
            {5, 1215000},
            {6, 3645000},
            {7, 10935000}
        };

        public static readonly Dictionary<string, bool> CONTACT_ACTIONS = new Dictionary<string, bool>
        {
            {"attack", true},
            {"heal", true},
            {"harvest", true},
            {"reserveController", true},
            {"rangedAttack", false},
            {"rangedHeal", false},
            {"build", false},
            {"repair", false},
            {"upgradeController", false}
        };
        
        public static readonly Dictionary<int, Color> FLAG_COLORS = new Dictionary<int, Color>
        {
            {1, new Color(.95f, .262f, .218f)},
            {2, new Color(0.6117f, 0.1529411764705882f, 0.6901960784313725f)},
            {3, new Color(0.1294117647058824f, 0.5882352941176471f, 0.9529411764705882f)},
            {4, new Color(0, 0.73725490196078432f, .83137f)},
            {5, new Color(0.2980392156862745f, 0.6862745098039216f, 0.3137254901960784f)},
            {6, new Color(1, 0.9215686274509804f, 0.2313725490196078f)},
            {7, new Color(1, 0.596078431372549f, 0)},
            {8, new Color(0.4745098039215686f, 0.3333333333333333f, 0.2823529411764706f)},
            {9, new Color(0.6196078431372549f, 0.6196078431372549f, 0.6196078431372549f)},
            {10, new Color(1f, 1f, 1f)},
        };
    }
}