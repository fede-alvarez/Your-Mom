using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Cards
    public static event UnityAction DrawCard;
    public static void OnDrawCard() => DrawCard?.Invoke();
    #endregion

    #region General
    public static event UnityAction TurnPassed;
    public static void OnTurnPassed() => TurnPassed?.Invoke();

    public static event UnityAction<GameManager.Turn> TurnChanged;
    public static void OnTurnChanged(GameManager.Turn turn) => TurnChanged?.Invoke(turn);

    public static event UnityAction PlayerLost;
    public static void OnPlayerLost() => PlayerLost?.Invoke();

    public static event UnityAction PlayerWon;
    public static void OnPlayerWon() => PlayerWon?.Invoke();
    #endregion
}
