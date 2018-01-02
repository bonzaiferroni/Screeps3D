using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    public static class PrefabLoader {

        private static Dictionary<string, GameObject> _cache = new Dictionary<string, GameObject>();
        private static Transform _parent;

        public static void Init()
        {
            _parent = new GameObject("prefabLoader").transform;
            _cache = new Dictionary<string, GameObject>();
        }

        public static GameObject Load(string path, Transform parent = null)
        {
            if (!_cache.ContainsKey(path))
            {
                _cache[path] = Resources.Load(path) as GameObject;
            }
            if (_cache[path])
            {
                if (parent == null)
                {
                    parent = _parent;
                }
                return Object.Instantiate(_cache[path], parent);
            }
            else
            {
                return null;
            }
        }

        public static GameObject Look(string path)
        {
            if (!_cache.ContainsKey(path))
            {
                _cache[path] = Resources.Load(path) as GameObject;
            }
            if (_cache[path])
            {
                return _cache[path];
            }
            else
            {
                return null;
            }
        }
    }
}
