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
    }
}