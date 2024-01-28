using UnityEngine;

public class PunchBox : MonoBehaviour
{
    private Player _player;

    void Start()
    {
        _player = GameManager.GetInstance.GetPlayer;
    }

    public void StartSound()
    {
        AudioManager.GetInstance.PlaySound(AudioManager.AudioList.CajaSorpresa);
    }

    public void Hit()
    {
        if (GameManager.GetInstance.IsPCTurn) return;
        print("HIT");
        AudioManager.GetInstance.PlaySound(AudioManager.AudioList.Punio);
        _player.Damage(4);
    }
}
