using UnityEngine;

[CreateAssetMenu(menuName = "Card asset/New Card")]
public class CardStats : ScriptableObject
{
    public string CardName;
    public CardDeck.CardTypes CardType;
    public int Damage;
    public int Cost;
    public Sprite Art;
}
