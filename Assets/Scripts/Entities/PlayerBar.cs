using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Player player;
    private int reputation;

    private float newRep;
    private float repu;
    public float GetReputation() {
        print("PLAYER  health in number: " + repu + "health in percentage: " + newRep);
        return repu;
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>(); // Assuming Player is the script attached to the parent GameObject
    }
    public void DoDamage(int newDamage){
        damageReceived = newDamage;
    }

    private void Start()
    {
        if (player != null)
        {
            reputation = player.GetReputation();
        }
    }

    // Update is called once per frame
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
        
    }
}