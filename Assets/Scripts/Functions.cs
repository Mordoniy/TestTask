using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    public static int GetPointsCard(CardType type)
    {
        int point = UISettings.Instance.cards[(int) type].startValue;
        if (PlayerPrefs.HasKey(type + "Point"))
        {
            point = PlayerPrefs.GetInt(type + "Point");
        }

        return point;
    }

    public static int GetPoints()
    {
        int point = UISettings.Instance.startPoint;
        if (PlayerPrefs.HasKey("Points"))
        {
            point = PlayerPrefs.GetInt("Points");
        }

        return point;
    }

    public static void SetPointsCard(CardType type, int value)
    {
        PlayerPrefs.SetInt(type + "Point", value);
    }

    public static void SetPoints(int value)
    {
        PlayerPrefs.SetInt("Points", value);
    }

    public static IEnumerator ChangeAlpha(float startAlpha, float targetAlpha, float time, List<Material> materials)
    {
        float maxTime = time;
        while (time > 0)
        {
            foreach (Material material in materials)
            {
                material.color = new Color(material.color.r, material.color.g, material.color.b,
                    targetAlpha - (targetAlpha - startAlpha) * (time / maxTime));
            }

            time -= Time.deltaTime;
            yield return null;
        }

        foreach (Material material in materials)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b,
                targetAlpha);
        }
    }
}