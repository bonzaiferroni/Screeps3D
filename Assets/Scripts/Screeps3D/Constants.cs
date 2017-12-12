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
        public const string TYPE_CONSTRUCTION = "constructionSite";
        public const string TYPE_LAB = "lab";
        public const string TYPE_CONSTRUCTED_WALL = "constructedWall";
        public const string TYPE_NUKER = "nuker";
        public const string TYPE_MINERAL = "mineral";
        public const string TYPE_POWER_SPAWN = "powerSpawn";
        public const string TYPE_SOURCE = "source";




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

        public static readonly Dictionary<string, float> CONSTRUCTION_COST = new Dictionary<string, float>
        {
            {"spawn", 15000},
            {"extension", 3000},
            {"road", 300},
            {"constructedWall", 1},
            {"rampart", 1},
            {"link", 5000},
            {"storage", 30000},
            {"tower", 5000},
            {"observer", 8000},
            {"powerSpawn", 100000},
            {"extractor", 5000},
            {"lab", 50000},
            {"terminal", 100000},
            {"container", 5000},
            {"nuker", 100000},
        };
        
        public static readonly Dictionary<float, float> MINERAL_DENSITY = new Dictionary<float, float>
        {
            {1, 15000},
            {2, 35000},
            {3, 70000},
            {4, 100000}
        };
        
        public static readonly HashSet<string> RESOURCES_ALL = new HashSet<string>()
        {
            "energy", 
            "power", 
            "H",
            "O",
            "U",
            "K",
            "L",
            "Z",
            "X",
            "G",
            "OH",
            "ZK",
            "UL",
            "UH",
            "UO",
            "KH",
            "KO",
            "LH",
            "LO",
            "ZH",
            "ZO",
            "GH",
            "GO",
            "UH2O",
            "UHO2",
            "KH2O",
            "KHO2",
            "LH2O",
            "LHO2",
            "ZH2O",
            "ZHO2",
            "GH2O",
            "GHO2",
            "XUH2O",
            "XUHO2",
            "XKH2O",
            "XKHO2",
            "XLH2O",
            "XLHO2",
            "XZH2O",
            "XZHO2",
            "XGH2O",
            "XGHO2",
        }; 
    }
}