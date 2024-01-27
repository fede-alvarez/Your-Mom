using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentTurn;

    void Start()
    {
        EventManager.TurnChanged += OnTurnChanged;
    }

    private void OnTurnChanged(GameManager.Turn turn)
    {
        _currentTurn.text = turn.ToString();
    }

    void OnDestroy()
    {
        EventManager.TurnChanged -= OnTurnChanged;
    }
}
