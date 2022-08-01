using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    [SerializeField] private ScrollRect panel;
    [SerializeField] private Slider slider;
    [SerializeField] private Image shadowRight;
    [SerializeField] private Image shadowLeft;
    [SerializeField] private Image arrowRight;
    [SerializeField] private Image arrowLeft;

    private void Start()
    {
        ChangeValue();
    }

    public void PanelChangePosition()
    {
        if (slider.value != panel.horizontalNormalizedPosition)
        {
            slider.value = panel.horizontalNormalizedPosition;
            ChangeValue();
        }
    }

    public void SliderChangePosition()
    {
        if (panel.horizontalNormalizedPosition != slider.value)
        {
            panel.horizontalNormalizedPosition = slider.value;
            ChangeValue();
        }
    }

    private void ChangeValue()
    {
        float rightA = (1 - Mathf.Clamp(slider.value, .9f, 1f)) / .1f;
        float leftA = Mathf.Clamp(slider.value, 0f, .1f) / .1f;
        shadowLeft.color = new Color(shadowLeft.color.r, shadowLeft.color.g, shadowLeft.color.b, leftA);
        arrowLeft.color = new Color(arrowLeft.color.r, arrowLeft.color.g, arrowLeft.color.b, leftA);
        shadowRight.color = new Color(shadowRight.color.r, shadowRight.color.g, shadowRight.color.b, rightA);
        arrowRight.color = new Color(arrowRight.color.r, arrowRight.color.g, arrowRight.color.b, rightA);
    }
}