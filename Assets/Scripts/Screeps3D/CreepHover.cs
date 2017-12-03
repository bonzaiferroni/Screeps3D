using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepHover : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _posRef;
    private float _nextRotationTarget;
    private float _nextPosTarget;

    private const float _posDrift = .025f;
    private const float _rotDrift = 5;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FindNewPosTarget();
        FindNewRotationTarget();
        ApproachTarget();
    }

    private void FindNewRotationTarget()
    {
        if (_nextRotationTarget > Time.time) return;
        _nextRotationTarget = Time.time + Random.value * 2;
        _targetRotation = Quaternion.Euler(RandomDrift(_rotDrift), RandomDrift(_rotDrift), RandomDrift(_rotDrift));
    }

    private void FindNewPosTarget()
    {
        if (_nextPosTarget > Time.time) return;
        _nextPosTarget = Time.time + Random.value;
        _targetPosition = new Vector3(RandomDrift(_posDrift), RandomDrift(_posDrift), RandomDrift(_posDrift));
    }

    private void ApproachTarget()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPosition, ref _posRef, 1);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, Time.deltaTime);
    }

    private float RandomDrift(float drift)
    {
        return (Random.value * drift * 2) - drift;
    }
}