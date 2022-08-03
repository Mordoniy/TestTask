using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "UISettings")]
public class UISettings : ScriptableObject
{
    private static UISettings instance;

    public static UISettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<UISettings>("UISettings");
            }

            return instance;
        }
    }
    
    public List<CardSettings> cards;
    public int startPoint;
    public float timeAnimationAddPoint;
    public int countAddPoint;
}