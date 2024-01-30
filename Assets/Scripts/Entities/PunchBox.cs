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
        AudioManager.GetInstance.PlaySound(AudioManager.AudioList.Punio);

        if (transform.parent.name == "EnemyInteractables")
            EventManager.OnPlayerDamage();
        else
            EventManager.OnEnemyDamage();
    }
}
