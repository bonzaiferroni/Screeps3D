using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    public static class PrefabLoader {

        private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();
        private static Transform _parent;

        public static GameObject Load(string path, Transform parent = null)
        {
            if (!cache.ContainsKey(path))
            {
                cache[path] = Resources.Load(path) as GameObject;
            }
            if (cache[path])
            {
                if (parent == null)
                {
                    if (_parent == null)
                    {
                        _parent = new GameObject("prefabLoader").transform;
                    }
                    parent = _parent;
                }
                return Object.Instantiate(cache[path], parent);
            }
            else
            {
                return null;
            }
        }

        public static GameObject Look(string path)
        {
            if (!cache.ContainsKey(path))
            {
                cache[path] = Resources.Load(path) as GameObject;
            }
            if (cache[path])
            {
                return cache[path];
            }
            else
            {
                return null;
            }
        }
    }
}
