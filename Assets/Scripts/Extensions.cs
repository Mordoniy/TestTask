using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public static class Extensions
{
    public static Tween DOColor(this SkeletonRenderer target, Color endValue, float duration)
    {
        return DOTween.To(() => target.skeleton.GetColor(),
            c => { target.skeleton.SetColor(c); },
            endValue,
            duration);
    }
}