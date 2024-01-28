using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private CardStats _cardStats;
    [Header("GUI")]
    [SerializeField] private TMP_Text _cardDamageLabel;

    private float _initPositionY;
    private bool _isSelected = false;
    private int _randomDamage = 10;
    private int _cardIndex;

    void Start()
    {
        _initPositionY = transform.position.y;
        _randomDamage = Mathf.CeilToInt(Random.Range(0, 1.0f) * 10) * 10;
        UpdateStats();
    }

    void OnMouseEnter()
    {
        if (_isSelected) return;
        transform.DOMoveY(0.1f, 0.3f).SetRelative();
    }

    void OnMouseExit()
    {
        if (_isSelected) return;
        transform.DOMoveY(_initPositionY, 0.3f);
    }

    void OnMouseDown()
    {
        if (GameManager.GetInstance.IsPCTurn) return;
        Use();
    }

    private void Use()
    {
        if (_isSelected) return;
        _isSelected = true;

        GameManager.GetInstance.SetPlayersCard(this);
        transform.DOMoveY(0.2f, 0.3f).SetRelative().OnComplete(Deactivate);
    }

    private void Deactivate()
    {
        EventManager.OnCardPlayed();
        GameManager.GetInstance.SetEnemysTurn();
        GameManager.GetInstance.SetBattleMode();
        //gameObject.SetActive(false);
    }

    private void UpdateStats()
    {
        _cardDamageLabel.text = _randomDamage.ToString();
    }

    public void SetIndex(int index)
    {
        _cardIndex = index;
    }

    public int GetDamage => _randomDamage;
    public int GetIndex => _cardIndex;

    public string CardName => _cardStats.CardName;
    public CardDeck.CardTypes CardType => _cardStats.CardType;
}