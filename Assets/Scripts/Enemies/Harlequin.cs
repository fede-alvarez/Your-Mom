using UnityEngine;

public class Harlequin : BaseEnemy
{
    [SerializeField] private Animator _animator;

    [SerializeField] private int _reputation = 100;
    public int GetReputation()
    {
        return _reputation;
    }

    void Start()
    {
        //Invoke("Throw", 2.0f);
    }

    public void Throw()
    {
        _animator.SetTrigger("Throw");
    }
}
