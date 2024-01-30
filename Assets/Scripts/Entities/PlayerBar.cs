using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerBar : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Player player;
    private int reputation;
    private int maxRep;
    private float newRep;
    private float repu;
    public float GetReputation()
    {
        //print("PLAYER  health in number: " + repu + "health in percentage: " + newRep);
        return repu;
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>(); // Assuming Player is the script attached to the parent GameObject
    }

    private void Start()
    {
        if (player != null)
        {
            reputation = player.GetReputation();
            maxRep = reputation;
        }
    }

    public void DoDamage(int newDamage)
    {
        damageReceived = newDamage;

        if (damageReceived <= 0) return;
        reputation = player.GetReputation();
        int z = reputation - damageReceived;
        newRep = (z * 100) / maxRep;
        player.SetReputation(z);
        repu = z;
        VisualFX(newRep / 100);
        damageReceived = 0;
    }

    private void VisualFX(float amount)
    {
        barFill.DOFillAmount(amount, 0.3f);
        //barFill.transform.DOShakeScale(0.2f, 1, 10);
        barFill.transform.parent.DOShakeScale(0.2f, .8f, 6);
    }
}