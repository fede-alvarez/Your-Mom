using UnityEngine;

public class Harlequin : BaseEnemy
{
    public enum Object
    {
        StopSign,
        Bottle,
        Pie
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _objectHolder;

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
}
