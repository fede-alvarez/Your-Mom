using UnityEngine;

public class PunchBox : MonoBehaviour
{
    private Player _player;

    void Start()
    {
        _player = GameManager.GetInstance.GetPlayer;
    }

    public void Hit()
    {
        if (!GameManager.GetInstance.IsPCTurn) return;
        _player.Damage(4);
    }
}
