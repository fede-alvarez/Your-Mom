using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerBar : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Player player;
    private int reputation;

    private float newRep;
    private float repu;
    public float GetReputation()
    {
        print("PLAYER  health in number: " + repu + "health in percentage: " + newRep);
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
        }
    }

    public void DoDamage(int newDamage)
    {
        damageReceived = newDamage;

        if (damageReceived <= 0) return;
        reputation = player.GetReputation();

        int z = reputation - damageReceived;
        newRep = (z * 100) / reputation;
        player.SetReputation(z);
        repu = z;
        barFill.DOFillAmount(newRep / 100, 0.3f);
        damageReceived = 0;
    }



    /*
    void Update()
    {
        if (damageReceived > 0){
            int z = reputation - damageReceived;
            newRep = (z * 100) / reputation;
            player.SetReputation(z);
            repu = z;
            if(newRep >= 0){
                repu = z;
                barFill.fillAmount = newRep / 100;
                damageReceived = 0;
            }else {
                repu = z;
                barFill.fillAmount = 0;
                damageReceived = 0;
            }
        }
        
    }*/
}