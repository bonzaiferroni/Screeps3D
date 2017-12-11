using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class PoolLoader
    {
        public static Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string, Stack<GameObject>>();

        public static GameObject Load(string path)
        {
            if (!_pools.ContainsKey(path))
            {
                _pools[path] = new Stack<GameObject>();
            }

            if (_pools[path].Count == 0)
            {
                var go = PrefabLoader.Look(path);
                if (!go)
                    throw new Exception(string.Format("no resource found at path: {0}", path));
                _pools[path].Push(UnityEngine.Object.Instantiate(go));
            }

            return _pools[path].Pop();
        }

        public static void Return(string path, GameObject go)
        {
            if (!_pools.ContainsKey(path))
            {
                throw new Exception("returned pooled object to unused pool");
            }

            _pools[path].Push(go);
        }
    }
}