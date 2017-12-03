using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadePanel : MonoBehaviour
{
    private CanvasGroup _cgroup;
    private float _target;
    private float _targetRef;

    private void Awake()
    {
        _cgroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (_cgroup.alpha == _target)
        {
            return;
        }

        _cgroup.alpha = Mathf.SmoothDamp(_cgroup.alpha, _target, ref _targetRef, .25f);
    }

    public void Show(bool show, bool instant = false)
    {
        if (show)
        {
            _target = 1;
            _cgroup.blocksRaycasts = true;
        } else
        {
            _cgroup.blocksRaycasts = false;
            _target = 0;
        }

        if (instant)
        {
            _cgroup.alpha = _target;
        }
    }

    public void Show()
    {
        Show(true);
    }

    public void Hide()
    {
        Show(false);
    }
}