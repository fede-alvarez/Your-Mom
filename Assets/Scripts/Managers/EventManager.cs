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
    #endregion
}
