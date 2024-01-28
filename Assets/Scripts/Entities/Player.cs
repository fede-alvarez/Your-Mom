using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Object
    {
        StopSign,
        Bottle,
        Pie
    }

    [SerializeField] private int _reputation = 1000;
    public int GetReputation()
    {
        return _reputation;
    }

    public void SetReputation(int re)
    {
        print("setting reputation of player to: " + re);
        _reputation = re;
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _objectHolder;
    private bool _isDead = false;

    public void Damage(int amount)
    {
        if (_isDead) return;

        _animator.SetTrigger("Damage");
        _reputation -= amount;

        if (_reputation <= 0)
            _isDead = true;
    }

    public void ActivateObject(Object obj)
    {
        DeactivateObjects();
        _objectHolder.GetChild((int)obj).gameObject.SetActive(true);
    }

    private void DeactivateObjects()
    {
        foreach (Transform child in _objectHolder)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Throw()
    {
        _animator.SetTrigger("Throw");
    }

    public void Crouch()
    {
        _animator.SetTrigger("Crouch");
    }

    public void FuckYou()
    {
        _animator.SetTrigger("FuckYou");
    }

    public void Roll()
    {
        _animator.SetTrigger("Roll");
    }
}
