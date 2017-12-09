using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PrefabLoader {

    private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    public static GameObject Load(string path)
    {
        if (!cache.ContainsKey(path))
        {
            cache[path] = Resources.Load(path) as GameObject;
        }
        if (cache[path])
        {
            return Object.Instantiate(cache[path]);
        }
        else
        {
            return null;
        }
    }
}
