using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _reputation = 100;
    private bool _isDead = false;

    public void Damage(int amount)
    {
        if (_isDead) return;

        _reputation -= amount;

        if (_reputation <= 0)
            _isDead = true;
    }
}
