using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private Transform _playerCardFinalPosition;

    [SerializeField] private ParticleSystem _fruitsParticles;

    public HarlequinBar harlequinBar;
    public PlayerBar playerBar;

    private Turn _currentTurn = Turn.Player;
    private bool _inBattle = false;

    private Card _enemyCurrentCard;
    private Card _playerCurrentCard;

    private bool _gamePaused = false;

    private bool _handWon = false;
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
        InvokeRepeating("SpawnFruits", 3.0f, 5.0f);
    }

    private void SpawnFruits()
    {
        AudioManager.GetInstance.PlaySound(AudioManager.AudioList.Risa, true);
        AudioManager.GetInstance.PlaySound(AudioManager.AudioList.Aplauso, true);

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

    public void onPlyWin()
    {
        _handWon = true;
        _enemyCurrentCard.Flash();
        if (harlequinBar.GetReputation() <= 0)
        {
            print("ENEMY DEAD");
            _enemyCurrentCard.transform.DOShakePosition(0.5f, 0.3f, 8);
            EventManager.OnPlayerWon();
        }
        else
        {
            _enemyCurrentCard.transform.DOShakePosition(0.5f, 0.3f, 8).OnComplete(StartNextBattle);
        }
    }
    public void onEnmWin()
    {
        _handWon = false;

        _playerCurrentCard.Flash();
        if (playerBar.GetReputation() <= 0)
        {
            print("PLAYER DEAD");
            _playerCurrentCard.transform.DOShakePosition(0.5f, 0.3f, 8);
            EventManager.OnPlayerLost();
        }
        else
        {
            _playerCurrentCard.transform.DOShakePosition(0.5f, 0.3f, 8).OnComplete(StartNextBattle);
        }
    }

    private void PlayerWins()
    {
        harlequinBar.DoDamage(_playerCurrentCard.GetDamage);
        EventManager.OnHandWon();
        Invoke("onPlyWin", 0.2f);
    }

    private void PlayerLose()
    {
        playerBar.DoDamage(_enemyCurrentCard.GetDamage);
        EventManager.OnHandLost();
        Invoke("onEnmWin", 0.2f);
    }

    private void StartNextBattle()
    {
        StartCoroutine("TurnActions");
    }

    private IEnumerator TurnActions()
    {
        Destroy(_enemyCurrentCard.gameObject);
        Destroy(_playerCurrentCard.gameObject);
        yield return new WaitForSeconds(1.0f);

        // Object animation
        if (_handWon)
            SetPlayerObjectAnimation();
        else
            SetProperObjectAnimation();

        yield return new WaitForSeconds(0.7f);
        GetRandomPlayerCard();
        yield return new WaitForSeconds(0.8f);
        GameManager.GetInstance.SetPlayersTurn();
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

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void TogglePause()
    {
        _gamePaused = !_gamePaused;

        if (_gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void GetRandomCard()
    {
        //print("Get Random Card!");
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
        //print("Set Enemy Turn");
        _currentTurn = Turn.PC;
        EventManager.OnTurnChanged(Turn.PC);
    }

    public bool IsPCTurn => _currentTurn == Turn.PC;

    public Player GetPlayer => _player;

    public Transform GetPlayerCardFinalPosition => _playerCardFinalPosition;

    public static GameManager GetInstance => _instance;
}
