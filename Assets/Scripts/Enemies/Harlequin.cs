using UnityEngine;

public class Harlequin : BaseEnemy
{
    [SerializeField] private int _reputation = 100;

    public enum Object
    {
        StopSign,
        Bottle,
        Pie
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _objectHolder;

    void Start()
    {
        EventManager.EnemyDamage += DamageAnimation;
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

    public void DamageAnimation()
    {
        _animator.SetTrigger("Damage");
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

    public int GetReputation()
    {
        return _reputation;
    }
    public void SetReputation(int re)
    {
        //print("setting reputation of harlequin to: " + re);
        _reputation = re;
    }

    void OnDestroy()
    {
        EventManager.EnemyDamage -= DamageAnimation;
    }
}
