using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Turn
    {
        Player,
        PC
    }

    [SerializeField] private CardDeck _gameDeck;
    [SerializeField] private BaseEnemy _currentEnemy;
    [SerializeField] private Transform _enemyInteractables;

    private Turn _currentTurn = Turn.Player;
    private bool _inBattle = false;

    private Card _enemyCurrentCard;
    private Card _playerCurrentCard;

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

    public void SetBattleMode()
    {
        _inBattle = true;
        Invoke("Result", .8f);
    }

    private void Result()
    {
        if (_enemyCurrentCard.CardType == _playerCurrentCard.CardType)
        {
            // Check by Value
            if (_enemyCurrentCard.GetDamage > _playerCurrentCard.GetDamage)
            {
                //print("Enemy WINS");
                PlayerLose();
            }
            else if (_enemyCurrentCard.GetDamage == _playerCurrentCard.GetDamage)
            {
                print("DRAW");
                StartNextBattle();
            }
            else
            {
                PlayerWins();
            }

            return;
        }

        // Fisica > Mental > Esquive
        switch (_enemyCurrentCard.CardType)
        {
            case CardDeck.CardTypes.Physical:
                if (_playerCurrentCard.CardType == CardDeck.CardTypes.Mental)
                {
                    PlayerLose();
                }
                else if (_playerCurrentCard.CardType == CardDeck.CardTypes.Elusive)
                {
                    PlayerWins();
                }
                break;
            case CardDeck.CardTypes.Mental:
                if (_playerCurrentCard.CardType == CardDeck.CardTypes.Physical)
                {
                    PlayerWins();
                }
                else if (_playerCurrentCard.CardType == CardDeck.CardTypes.Elusive)
                {
                    PlayerLose();
                }
                break;
            case CardDeck.CardTypes.Elusive:
                if (_playerCurrentCard.CardType == CardDeck.CardTypes.Physical)
                {
                    PlayerLose();
                }
                else if (_playerCurrentCard.CardType == CardDeck.CardTypes.Mental)
                {
                    PlayerWins();
                }
                break;
        }
    }

    private void PlayerWins()
    {
        _enemyCurrentCard.transform.DOShakePosition(0.5f, 0.3f, 8).OnComplete(StartNextBattle);
    }

    private void PlayerLose()
    {
        _playerCurrentCard.transform.DOShakePosition(0.5f, 0.3f, 8).OnComplete(StartNextBattle);
    }

    private void StartNextBattle()
    {
        Destroy(_enemyCurrentCard.gameObject);
        Destroy(_playerCurrentCard.gameObject);

        // Object animation
        SetProperObjectAnimation();

        GameManager.GetInstance.SetPlayersTurn();
    }

    private void SetProperObjectAnimation()
    {
        switch (_enemyCurrentCard.CardName)
        {
            case "Actually":
                break;
            case "Bottle":
                (_currentEnemy as Harlequin).ActivateObject(Harlequin.Object.Bottle);
                break;
            case "Crouch":
                break;
            case "FYou":
                break;
            case "NoYou":
                break;
            case "Pie":
                (_currentEnemy as Harlequin).ActivateObject(Harlequin.Object.Pie);
                break;
            case "Punch":
                _enemyInteractables.GetChild(0).gameObject.SetActive(true);
                break;
            case "Roll":
                break;
            case "Your mom":
                break;
        }
    }

    public void GetRandomCard()
    {
        _enemyCurrentCard = _gameDeck.DrawSingleCard();
    }

    public void SetPlayersCard(Card card)
    {
        _playerCurrentCard = card;
    }

    public void PlayGame()
    {
        _gameDeck.Shuffle();
        SetPlayersTurn();
    }

    public void SetPlayersTurn()
    {
        //print("Players Turn");
        _currentTurn = Turn.Player;
        EventManager.OnTurnChanged(Turn.Player);
    }

    public void SetEnemysTurn()
    {
        //print("Enemy Turn");
        _currentTurn = Turn.PC;
        EventManager.OnTurnChanged(Turn.PC);
    }

    public bool IsPCTurn => _currentTurn == Turn.PC;

    public static GameManager GetInstance => _instance;
}
