using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Object
    {
        StopSign,
        Bottle,
        Pie
    }

    [SerializeField] private int _reputation = 100;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _objectHolder;
    private bool _isDead = false;

    public void Damage(int amount)
    {
        if (_isDead) return;

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
