using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleCalculate
{
    public static float GetDamageAttack(CharacterParameters attacking, CharacterParameters target)
    {
        float koef = 1;
        if (attacking.attack != 0 && target.defence != 0)
            koef = (float) attacking.attack / target.defence;
        else
        {
            if (attacking.attack == 0)
                koef = 0;
            if (target.defence == 0)
                koef = 3;
        }

        if (attacking.attack == target.defence)
            koef = 1;

        koef = Mathf.Clamp(koef, 1f / 3, 3);

        return attacking.damage * koef;
    }
}