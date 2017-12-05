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
            if (queue.Count == 0)
                return;

            var action = queue.Dequeue();
            action();
        }
    }
}