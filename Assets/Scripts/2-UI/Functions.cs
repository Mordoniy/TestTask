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
}