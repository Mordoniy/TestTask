using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings", fileName = "Settings")]
public class Settings : ScriptableObject
{
    private static Settings instance;

    public string commonSkin;
    public string eliteSkin;
    public string bloodSkin;

    public float offsetCharacter;

    public CharacterParameters commonCharacter;
    public CharacterParameters eliteCharacter;

    public static Settings Instance
    {
        get
        {
            if (!instance)
                instance = Resources.Load<Settings>("Settings");
            return instance;
        }
    }
}