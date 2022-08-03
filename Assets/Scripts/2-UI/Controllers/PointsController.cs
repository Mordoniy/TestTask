using System.Collections;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private RectTransform star;

    [SerializeField] private AnimationCurve easeOutBack;
    [SerializeField] private AnimationCurve easeOutQuad;

    private UISettings settings;

    private Coroutine currentEffect;

    private void Start()
    {
        settings = Resources.Load<UISettings>("UISettings");
    }

    public void Init()
    {
        text.text = Functions.GetPoints().ToString();
    }

    public void AddPoints(int value)
    {
        int currentValue = int.Parse(text.text);
        if (currentEffect != null)
            StopCoroutine(currentEffect);
        currentEffect = StartCoroutine(AddPointEffect(currentValue, Functions.GetPoints()));
    }

    IEnumerator AddPointEffect(float start, float target)
    {
        float time = 0;
        float maxTime = settings.timeAnimationAddPoint;

        while (time < maxTime)
        {
            time += Time.deltaTime;
            star.eulerAngles = new Vector3(0, 0, 360 * easeOutQuad.Evaluate(time / maxTime));
            text.rectTransform.localScale = Vector3.one * easeOutBack.Evaluate(time / maxTime);
            text.text = Mathf.RoundToInt(start + (target - start) * easeOutQuad.Evaluate(time / maxTime)).ToString();
            yield return null;
        }

        star.eulerAngles = Vector3.zero;
        text.rectTransform.localScale = Vector3.one;
        text.text = target.ToString();

        yield return null;
    }
}