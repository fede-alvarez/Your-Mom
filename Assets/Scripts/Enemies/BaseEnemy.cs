using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats;
    private bool _isDead = false;

    public void Damage(int amount)
    {
        if (_isDead) return;

        _stats.Hp -= amount;
        if (_stats.Hp <= 0)
            _isDead = true;
    }

    public void Attack()
    {

    }
}
