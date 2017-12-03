using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObserver : MonoBehaviour
{
    private const float MAX_RANDOM_DELTA = 80;
    private const float TARGET_DELAY = 1;

    private float _nextTarget;
    private Quaternion[] _rotations;
    private Quaternion _target;

    // Use this for initialization
    void Start()
    {
        _rotations = new Quaternion[5];
        var initial = transform.rotation.eulerAngles;
        for (var i = 0; i < 5; i++)
        {
            _rotations[i] = Quaternion.Euler(Randomize(initial.x), Randomize(initial.y), initial.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindNewTarget();
        RotateTowardTarget();
    }

    private void RotateTowardTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _target, Time.deltaTime);
    }

    private void FindNewTarget()
    {
        if (_nextTarget > Time.time) return;
        _nextTarget = Time.time + TARGET_DELAY;
        _target = _rotations[(int) (_rotations.Length * Random.value)];
    }

    private float Randomize(float value)
    {
        return value + (Random.value * MAX_RANDOM_DELTA * 2) - MAX_RANDOM_DELTA;
    }
}