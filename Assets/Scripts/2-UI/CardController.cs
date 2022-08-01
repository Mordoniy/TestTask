using TMPro;
using UnityEngine;

public enum CardType
{
    Dinos,
    Numbers,
    SolarSystem,
    Alphabet,
    Alice,
}

public class CardController : MonoBehaviour
{
    public System.Action<CardType> onPlay;

    [SerializeField] private TMP_Text currentValue;
    [SerializeField] private TMP_Text maxValue;
    private CardType type;

    public void Init(CardType type, float current, float max)
    {
        this.type = type;
        currentValue.text = current.ToString();
        maxValue.text = max.ToString();
    }

    public void Play()
    {
        onPlay?.Invoke(type);
        currentValue.text = Functions.GetPointsCard(type).ToString();
    }
}