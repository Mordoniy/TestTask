using System.Collections;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private RectTransform star;

    [SerializeField] private AnimationCurve easeOutBack;
    [SerializeField] private AnimationCurve easeOutQuad;

    private Coroutine currentEffect;
    private int currentValue;

    public void Init()
    {
        currentValue = Functions.GetPoints();
        text.text = currentValue.ToString();
    }

    public void AddPoints(int value)
    {
        if (currentEffect != null)
            StopCoroutine(currentEffect);
        currentEffect = StartCoroutine(AddPointEffect(currentValue, Functions.GetPoints()));
    }

    IEnumerator AddPointEffect(float start, float target)
    {
        float time = 0;
        float maxTime = UISettings.Instance.timeAnimationAddPoint;

        while (time < maxTime)
        {
            time += Time.deltaTime;
            star.eulerAngles = new Vector3(0, 0, 360 * easeOutQuad.Evaluate(time / maxTime));
            text.rectTransform.localScale = Vector3.one * easeOutBack.Evaluate(time / maxTime);
            currentValue = Mathf.RoundToInt(start + (target - start) * easeOutQuad.Evaluate(time / maxTime));
            text.text = currentValue.ToString();
            yield return null;
        }

        star.eulerAngles = Vector3.zero;
        text.rectTransform.localScale = Vector3.one;
        currentValue = (int) target;
        text.text = currentValue.ToString();

        yield return null;
    }
}