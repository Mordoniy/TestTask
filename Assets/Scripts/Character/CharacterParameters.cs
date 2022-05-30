using System;

public enum CharacterType
{
    Common,
    Elite,
}

public enum CharacterDir
{
    Left,
    Right,
}

[Serializable]
public struct CharacterParameters
{
    public CharacterType type;
    [NonSerialized] public CharacterDir dir;

    public int life;
    public int attack;
    public int defence;
    public int damage;

    [NonSerialized] public float currentLife;
}