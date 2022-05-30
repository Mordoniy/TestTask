using System;
using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private SkeletonMecanim skeleton;
    private new MeshRenderer renderer;
    private Animator animator;
    private string skin;

    private Tween currentTweenColor;

    public void Init(CharacterType type, CharacterDir dir, int numb)
    {
        skeleton = GetComponent<SkeletonMecanim>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<MeshRenderer>();

        skeleton.skeleton.SetColor(Color.gray);
        renderer.sortingOrder = -numb;

        switch (type)
        {
            case CharacterType.Common:
                skin = "base";
                break;
            case CharacterType.Elite:
                skin = "elite";
                break;
        }

        switch (dir)
        {
            case CharacterDir.Left:
                transform.localPosition = new Vector3(-Settings.Instance.offsetCharacter * numb, 0, 0);
                break;
            case CharacterDir.Right:
                skeleton.skeleton.scaleX = -1;
                transform.localPosition = new Vector3(Settings.Instance.offsetCharacter * numb, 0, 0);
                break;
        }

        SetSkin(skin);
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
        {
            currentTweenColor = skeleton.DOColor(Color.white, .3f);
            renderer.sortingLayerName = "SelectCharacters";
        }
        else
        {
            currentTweenColor = skeleton.DOColor(Color.grey, .3f);
            renderer.sortingLayerName = "Characters";
        }
    }

    public void Death()
    {
        //Заглушка, анимация отсутствует
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