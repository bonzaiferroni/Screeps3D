using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleLine : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI text;
    private float target;
    private float targetRef;

    private float Alpha {
        get { return text.color.a; }
        set {
            var color = text.color;
            color.a = value;
            text.color = color;
        }
    }

    public string Text {
        get { return text.text; }
        set {
            text.text = value;
            text.parseCtrlCharacters = true;
        }
    }

    public Color Color {
        get { return text.color; }
        set {
            text.color = value;
            Alpha = 0;
        }
    }

    public void Hide() {
        Alpha = 0;
        target = 0;
    }

    public void Show() {
        target = 1;
    }

    public void Update() {
        if (Alpha == target) {
            return;
        }

        Alpha = Mathf.SmoothDamp(Alpha, target, ref targetRef, .2f);
    }
}