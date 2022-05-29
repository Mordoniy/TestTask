using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Common,
    Elite,
}

public struct CharacterParameters
{
    public CharacterType type;
    
    public int life;
    public int attack;
    public int defence;
    public int damage;

    public float currentLife;
}
