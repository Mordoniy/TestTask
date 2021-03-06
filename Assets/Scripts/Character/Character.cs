using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static event System.Action<Character> CharacterDeath;

    public CharacterAnimator animator;
    public CharacterParameters parameters;

    private System.Action currentAttack;

    public bool IsDeath
    {
        get => parameters.currentLife == 0;
    }

    public void Init(CharacterParameters parameters, int numb)
    {
        this.parameters = parameters;
        animator.Init(this.parameters.type, this.parameters.dir, numb);
        this.parameters.currentLife = this.parameters.life;
    }

    public void Attack(Character target)
    {
        currentAttack = () =>
        {
            float damageValue = BattleCalculate.GetDamageAttack(parameters, target.parameters);
            target.Damage(damageValue);
        };
        animator.Attack(parameters.type);
    }

    public void Damage(float value)
    {
        parameters.currentLife = Mathf.Clamp(parameters.currentLife - value, 0, parameters.life);
        animator.Damage();
        if (parameters.currentLife == 0)
            Death();
    }

    public void SetSelect(bool isSelect)
    {
        animator.SetSelect(isSelect);
    }

    public void Hit()
    {
        currentAttack?.Invoke();
    }

    void Death()
    {
        animator.Death();
        Destroy(gameObject, 1); //Здесь должен использоваться пул менеджер
        CharacterDeath?.Invoke(this);
    }
}