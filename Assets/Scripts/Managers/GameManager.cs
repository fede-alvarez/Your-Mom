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
    [SerializeField] private Player _player;
    [SerializeField] private Transform _enemyInteractables;
    [SerializeField] private Transform _playerInteractables;
    [SerializeField] private ParticleSystem _fruitsParticles;

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
        DeactivateAllObjects();
        PlayGame();
        _fruitsParticles.Stop();
        InvokeRepeating("SpawnFruits", 3.0f, 5.0f);
    }

    private void SpawnFruits()
    {
        _fruitsParticles.Play();
        Invoke("StopFruits", Random.Range(1, 3));
    }

    private void StopFruits()
    {
        _fruitsParticles.Stop();
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
        SetPlayerObjectAnimation();

        GameManager.GetInstance.SetPlayersTurn();

        GetRandomPlayerCard();
    }

    private void SetProperObjectAnimation()
    {
        //DeactivateAllObjects();
        Harlequin enemy = (_currentEnemy as Harlequin);

        switch (_enemyCurrentCard.CardName)
        {
            case "Actually":
                enemy.FuckYou();
                break;
            case "Bottle":
                enemy.ActivateObject(Harlequin.Object.Bottle);
                enemy.Throw();
                break;
            case "Crouch":
                enemy.Crouch();
                break;
            case "FYou":
                enemy.FuckYou();
                break;
            case "NoYou":
                break;
            case "Pie":
                enemy.ActivateObject(Harlequin.Object.Pie);
                enemy.Throw();
                break;
            case "Punch":
                _enemyInteractables.GetChild(0).gameObject.SetActive(true);
                Invoke("DeactivateAllObjects", 3.0f);
                break;
            case "Roll":
                enemy.Roll();
                break;
            case "Your mom":
                enemy.FuckYou();
                break;
        }
    }

    private void SetPlayerObjectAnimation()
    {
        //DeactivateAllObjects();

        switch (_playerCurrentCard.CardName)
        {
            case "Actually":
                _player.FuckYou();
                break;
            case "Bottle":
                _player.ActivateObject(Player.Object.Bottle);
                _player.Throw();
                break;
            case "Crouch":
                _player.Crouch();
                break;
            case "FYou":
                _player.FuckYou();
                break;
            case "NoYou":
                break;
            case "Pie":
                _player.ActivateObject(Player.Object.Pie);
                _player.Throw();
                break;
            case "Punch":
                _playerInteractables.GetChild(0).gameObject.SetActive(true);
                Invoke("DeactivateAllObjects", 3.0f);
                break;
            case "Roll":
                _player.Roll();
                break;
            case "Your mom":
                _player.FuckYou();
                break;
        }
    }

    private void DeactivateAllObjects()
    {
        foreach (Transform enemyItem in _enemyInteractables)
        {
            enemyItem.gameObject.SetActive(false);
        }

        foreach (Transform playerItem in _playerInteractables)
        {
            playerItem.gameObject.SetActive(false);
        }
    }

    public void GetRandomCard()
    {
        _enemyCurrentCard = _gameDeck.DrawSingleCard();
    }

    public void GetRandomPlayerCard()
    {
        _gameDeck.DrawCard(_playerCurrentCard.GetIndex);
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

    public Player GetPlayer => _player;

    public static GameManager GetInstance => _instance;
}
