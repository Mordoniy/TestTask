using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private SkeletonMecanim skeleton;
    private Animator animator;
    private string skin;

    private Tween currentTweenColor;

    void Start()
    {
        skeleton = GetComponent<SkeletonMecanim>();
        animator = GetComponent<Animator>();

        skeleton.skeleton.SetColor(Color.gray);
        skin = skeleton.skeleton.skin.name;
    }

    public void Attack(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Common:
                animator.Play("Attack");
                break;
            case CharacterType.Elite:
                animator.Play("DoubleShift");
                break;
        }
    }

    public void Damage()
    {
        animator.Play("Damage");
        SetSkin("blood");
        StartCoroutine(ActionAfterAnimation("Damage", () => { SetSkin(skin); }));
    }

    public void SetSelect(bool isSelect)
    {
        if (isSelect)
            currentTweenColor = skeleton.DOColor(Color.white, .3f);
        else currentTweenColor = skeleton.DOColor(Color.grey, .3f);
    }

    void SetSkin(string name)
    {
        skeleton.skeleton.SetSkin(name);
        skeleton.skeleton.SetSlotsToSetupPose();
    }

    public IEnumerator ActionAfterAnimation(string name, System.Action action)
    {
        while (true)
        {
            yield return null; //Пропускаем один кадр для применения изменений
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(name))
            {
                action?.Invoke();
                yield break;
            }
        }
    }
}