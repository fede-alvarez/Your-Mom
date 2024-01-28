using UnityEngine;
using TMPro;
using DG.Tweening;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentTurn;
    [SerializeField] private CanvasGroup _wonScreen;
    [SerializeField] private CanvasGroup _lostScreen;
    [SerializeField] private CanvasGroup _resultFeedbackGroup;

    void Start()
    {
        EventManager.TurnChanged += OnTurnChanged;
        EventManager.PlayerLost += OnPlayerLost;
        EventManager.PlayerWon += OnPlayerWon;

        EventManager.HandWon += OnHandWon;
    }

    private void OnTurnChanged(GameManager.Turn turn)
    {
        //print("TURN CHANGED");
        _currentTurn.text = turn.ToString() + " Turn!";
    }

    private void OnHandWon()
    {
        if (_resultFeedbackGroup.TryGetComponent(out TMP_Text handText))
        {
            handText.text = "Sick!";
        }
        _resultFeedbackGroup.DOFade(1.0f, 0.4f).OnComplete(() =>
        {
            _resultFeedbackGroup.DOFade(0, 0.4f);
        });
    }

    private void OnHandLost()
    {
        if (_resultFeedbackGroup.TryGetComponent(out TMP_Text handText))
        {
            handText.text = "Boooo!";
        }
        _resultFeedbackGroup.DOFade(1.0f, 0.4f).OnComplete(() =>
        {
            _resultFeedbackGroup.DOFade(0, 0.4f);
        });
    }

    private void OnPlayerLost()
    {
        _lostScreen.gameObject.SetActive(true);
    }

    private void OnPlayerWon()
    {
        _wonScreen.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        EventManager.TurnChanged -= OnTurnChanged;
        EventManager.PlayerLost -= OnPlayerLost;
        EventManager.PlayerWon -= OnPlayerWon;

        EventManager.HandWon -= OnHandWon;
    }
}
