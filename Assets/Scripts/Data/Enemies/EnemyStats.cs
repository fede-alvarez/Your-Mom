using UnityEngine;

[CreateAssetMenu(menuName = "Enemy asset/New Enemy")]
public class EnemyStats : ScriptableObject
{
    public string EnemyName;
    public CardDeck.CardTypes CardType;
    public int Hp;
}

