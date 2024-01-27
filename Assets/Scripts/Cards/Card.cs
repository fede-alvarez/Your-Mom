using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private CardStats _cardStats;

    [Header("GUI")]
    [SerializeField] private TMP_Text _cardDamageLabel;
    private BoxCollider _collider;
    private bool _isAnimating = false;

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        UpdateStats();
    }

    void OnMouseDown()
    {
        if (GameManager.GetInstance.IsPCTurn) return;
        Use();
    }

    void OnMouseEnter()
    {
        if (GameManager.GetInstance.IsPCTurn) return;
        transform.DOMoveY(4.1f, 0.3f);
    }

    void OnMouseExit()
    {
        if (GameManager.GetInstance.IsPCTurn) return;
        transform.DOMoveY(4.07f, 0.3f);
    }

    public void Use()
    {
        _collider.enabled = false;
        print(_cardStats.CardName + " -> " + _cardStats.Damage);
        transform.DOMoveY(4.15f, 0.5f).OnComplete(Kill);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    #region Visuals
    private void UpdateStats()
    {
        _cardDamageLabel.text = _cardStats.Damage.ToString();
    }
    #endregion

    public CardDeck.CardTypes CardType => _cardStats.CardType;
}