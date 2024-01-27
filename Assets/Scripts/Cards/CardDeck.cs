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
    private int _totalCards = 5;

    void Start()
    {
        CreateDeck();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            CreateDeck();
        }
    }
    public GameObject decko; // Reference to the parent GameObject
    float cardSpacing = 0.35f;

    public Vector3 rotation = new Vector3(0f, 180f, 0f);
    public Vector3 deckoRotation = new Vector3(0f, 0f, 0f);


    public void CreateDeck()
    {
        // Clear the deck to avoid duplicates
        _deck.Clear();
        foreach (Transform child in decko.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        Card firstCardPrefab = _cardTypes[Random.Range(0, _cardTypes.Count)];
        Card firstCardInstance = Instantiate(firstCardPrefab, transform.position, Quaternion.identity);
        firstCardInstance.transform.SetParent(decko.transform);
        firstCardInstance.transform.Rotate(rotation);
        _deck.Add(firstCardInstance);

        for (int i = 1; i < _totalCards; i++)
        {
            float xPos = i * cardSpacing;
            Vector3 cardPosition = decko.transform.position + new Vector3(xPos, 0f, 0f);

            Card cardPrefab = _cardTypes[Random.Range(0, _cardTypes.Count)];
            Card cardInstance = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
            cardInstance.transform.Rotate(rotation);

            cardInstance.transform.SetParent(decko.transform);

            _deck.Add(cardInstance);

        }
        decko.transform.Rotate(deckoRotation);
    }



    //_deck.Add(_cardTypes[Random.Range(0, _cardTypes.Count - 1)]);
    //Debug.Log(_deck[i].CardType);

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
