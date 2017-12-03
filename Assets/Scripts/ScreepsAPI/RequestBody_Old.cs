using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class RequestBody_Old : Dictionary<string, string>
{
    public string ToQueryString()
    {
        var count = 0;
        var str = "?";
        foreach (var kvp in this)
        {
            str += string.Format("{0}={1}", kvp.Key, kvp.Value);
            count++;
            if (count < this.Count)
            {
                str += "&";
            }
        }
        return str;
    }
}