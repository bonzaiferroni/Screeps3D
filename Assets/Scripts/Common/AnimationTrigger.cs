using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Animator _anim;
    private int _speedHash;
    private int _jumpHash;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        foreach (var parameter in _anim.parameters)
        {
            if (parameter.name == "Speed")
            {
                _speedHash = parameter.nameHash;
            }
            if (parameter.name == "Jump")
            {
                _jumpHash = parameter.nameHash;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        if (_speedHash == 0)
        {
            return;
        }
        _anim.SetFloat(_speedHash, speed);
    }

    public void TriggerJump()
    {
        if (_jumpHash == 0)
        {
            return;
        }
        _anim.SetTrigger(_jumpHash);
    }
}