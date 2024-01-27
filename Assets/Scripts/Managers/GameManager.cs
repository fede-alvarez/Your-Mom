using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Turn
    {
        Player,
        PC
    }

    [SerializeField] private CardDeck _gameDeck;
    [SerializeField] private List<BaseEnemy> _enemies;

    private BaseEnemy _currentEnemy;
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
        _currentEnemy = _enemies[0];
        _gameDeck.Shuffle();
        SetPlayersTurn();
    }

    public void SetPlayersTurn()
    {
        _currentTurn = Turn.Player;
        EventManager.OnTurnChanged(Turn.Player);
    }

    public void SetEnemysTurn()
    {
        _currentTurn = Turn.PC;
        EventManager.OnTurnChanged(Turn.PC);
    }

    public Turn GetTurn => _currentTurn;

    public bool IsPCTurn => _currentTurn == Turn.PC;

    public BaseEnemy CurrentEnemy => _currentEnemy;

    public static GameManager GetInstance => _instance;
}
