using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public List<CardController> cards;
    public PointsController points;

    private UISettings settings;

    private void Start()
    {
        settings = UISettings.Instance;
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Init(settings.cards[i].type, Functions.GetPointsCard(settings.cards[i].type),
                settings.cards[i].maxValue);
            cards[i].onPlay = AddPoints;
        }
        points.Init();
    }

    void AddPoints(CardType type)
    {
        if (Functions.GetPointsCard(type) >= settings.cards[(int) type].maxValue)
        {
            return;
        }

        Functions.SetPointsCard(type, Functions.GetPointsCard(type) + 1);
        Functions.SetPoints(Functions.GetPoints() + settings.countAddPoint);
        points.AddPoints(settings.countAddPoint);
    }

    public void Restore()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Functions.SetPointsCard((CardType) i, settings.cards[i].startValue);
            cards[i].Init(settings.cards[i].type, settings.cards[i].startValue, settings.cards[i].maxValue);
        }
        Functions.SetPoints(settings.startPoint);
        points.Init();
    }
}