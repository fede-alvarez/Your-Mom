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

    void Start()
    {
        _initPositionY = transform.position.y;

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
        print(gameObject.name);
        _isSelected = true;
        transform.DOMoveY(0.3f, 0.3f).SetRelative().OnComplete(Deactivate);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void UpdateStats()
    {
        _cardDamageLabel.text = _cardStats.Damage.ToString();
    }

    public CardDeck.CardTypes CardType => _cardStats.CardType;
}