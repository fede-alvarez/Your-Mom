using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private CardStats _cardStats;
    [Header("GUI")]
    [SerializeField] private TMP_Text _cardDamageLabel;

    void Start()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        _cardDamageLabel.text = _cardStats.Damage.ToString();
    }

    public CardDeck.CardTypes CardType => _cardStats.CardType;
}