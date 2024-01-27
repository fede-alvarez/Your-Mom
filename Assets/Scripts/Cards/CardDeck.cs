using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public enum CardTypes
    {
        Physical,
        Mental,
        Elusive
    }

    [SerializeField] private List<Card> _cardTypes = new List<Card>();

    private List<Card> _deck = new List<Card>();
    private List<Card> _discardPile = new List<Card>();
    private int _totalCards = 50;

    void Start()
    {
        CreateDeck();
    }

    public void CreateDeck()
    {
        for (int i = 0; i < _totalCards; i++)
        {
            _deck.Add(_cardTypes[Random.Range(0, _cardTypes.Count - 1)]);
            //Debug.Log(_deck[i].CardType);
        }
    }

    public void Shuffle()
    {
        for (int i = _deck.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);

            Card tempCard = _deck[i];

            _deck[i] = _deck[rnd];
            _deck[rnd] = tempCard;
        }
    }

    public Card DrawCard()
    {
        if (_deck.Count == 0) return null;

        Card card = _deck[0];
        _deck.RemoveAt(0);
        _discardPile.Add(card);

        return card;
    }
}
