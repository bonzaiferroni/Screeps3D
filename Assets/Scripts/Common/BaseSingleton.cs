using System;
using UnityEngine;

namespace Common
{
    public class BaseSingleton<T>: MonoBehaviour where T: Component
    {
        private static T _instance;
        public static T Instance 
        {
            get {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();

                if (_instance != null) return _instance;

                throw new Exception(string.Format("expecting singleton of type {0} in scene", typeof(T)));
            }
        }
 
        public virtual void Awake ()
        {
            if (_instance == null) {
                _instance = this as T;
            } 
            else
            {
                throw new Exception(string.Format("multiple singletons of type {0}", typeof(T)));
            }
        }
    }
}