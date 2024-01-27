using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Turn
    {
        Player,
        PC
    }

    [SerializeField] private CardDeck _gameDeck;

    private Turn _currentTurn = Turn.Player;

    private static GameManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        PlayGame();
    }

    public void PlayGame()
    {
        _gameDeck.Shuffle();
        SetPlayersTurn();
    }

    public void SetPlayersTurn()
    {
        print("Players Turn");
        _currentTurn = Turn.Player;
        EventManager.OnTurnChanged(Turn.Player);
    }

    public void SetEnemysTurn()
    {
        print("Enemy Turn");
        _currentTurn = Turn.PC;
        EventManager.OnTurnChanged(Turn.PC);
    }

    public bool IsPCTurn => _currentTurn == Turn.PC;

    public static GameManager GetInstance => _instance;
}
