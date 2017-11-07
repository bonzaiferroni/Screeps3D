using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadePanel : MonoBehaviour {
    private CanvasGroup cgroup;
    private float target;
    private float targetRef;

    private void Awake() {
        cgroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (cgroup.alpha == target) {
            return;
        }

        cgroup.alpha = Mathf.SmoothDamp(cgroup.alpha, target, ref targetRef, .25f);
    }

    public void Show(bool show, bool instant = false) {
        if (show) {
            target = 1;
        } else {
            target = 0;
        }

        if (instant) {
            cgroup.alpha = target;
        }
    }

    public void Show() {
        Show(true);
    }

    public void Hide() {
        Show(false);
    }
}