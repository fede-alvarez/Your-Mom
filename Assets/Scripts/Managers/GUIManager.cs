using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentTurn;
    [SerializeField] private CanvasGroup _wonScreen;
    [SerializeField] private CanvasGroup _lostScreen;

    void Start()
    {
        EventManager.TurnChanged += OnTurnChanged;
        EventManager.PlayerLost += OnPlayerLost;
        EventManager.PlayerWon += OnPlayerWon;
    }

    private void OnTurnChanged(GameManager.Turn turn)
    {
        //print("TURN CHANGED");
        _currentTurn.text = turn.ToString();
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
    }
}
