using UnityEngine;

public class Harlequin : BaseEnemy
{
    [SerializeField] private Animator _animator;


    void Start()
    {
        //Invoke("Throw", 2.0f);
    }

    public void Throw()
    {
        _animator.SetTrigger("Throw");
    }
}
