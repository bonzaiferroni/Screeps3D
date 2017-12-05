using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class SchedulerTest : BaseSingleton<SchedulerTest>
    {
        
        private ActionQueue queue = new ActionQueue();
        
        public void Add(UnityAction action)
        {
            queue.Enqueue(action);
        }

        public void Delay(UnityAction action, float seconds)
        {
            StartCoroutine(DelaySeconds(action, seconds));
        }
        
        private IEnumerator DelaySeconds(UnityAction action, float delay) {
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
    
    [Serializable]
    public class ActionQueue : Queue<UnityAction> { }
}