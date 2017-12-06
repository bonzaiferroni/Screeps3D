using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Scheduler : BaseSingleton<Scheduler>
    {
        
        private Queue<Action> queue = new Queue<Action>();
        
        public void Add(Action action)
        {
            queue.Enqueue(action);
        }

        public void Delay(Action action, float seconds)
        {
            StartCoroutine(DelaySeconds(action, seconds));
        }
        
        private IEnumerator DelaySeconds(Action action, float delay) {
            yield return new WaitForSeconds(delay);
            queue.Enqueue(action);
        }

        private void Update()
        {
            var start = DateTime.Now.Millisecond;
            var current = DateTime.Now.Millisecond;
            while (queue.Count > 0 && current >= start && current - start < 10)
            {
                var action = queue.Dequeue();
                action();
                current = DateTime.Now.Millisecond;
            }
        }
    }
}