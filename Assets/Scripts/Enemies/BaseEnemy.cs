using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats;
    private bool _isDead = false;

    void Awake()
    {
        EventManager.TurnChanged += OnTurnChanged;
    }

    public void Damage(int amount)
    {
        if (_isDead) return;

        _stats.Hp -= amount;
        if (_stats.Hp <= 0)
            _isDead = true;
    }

    private void OnTurnChanged(GameManager.Turn turn)
    {
        if (turn == GameManager.Turn.Player) return;
        StartTurn();
    }

    public void StartTurn()
    {
        GameManager.GetInstance.GetRandomCard();
    }

    public void Attack()
    {

    }

    void OnDestroy()
    {
        EventManager.TurnChanged -= OnTurnChanged;
    }
}
