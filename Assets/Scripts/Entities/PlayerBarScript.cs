using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarScript : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Player player;
    private int reputation;

    private void Awake()
    {
        player = GetComponentInParent<Player>(); // Assuming Player is the script attached to the parent GameObject
    }

    private void Start()
    {
        if (player != null)
        {
            reputation = player.GetReputation();
            print(reputation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (damageReceived > 0){
            int z = reputation - damageReceived;
            float newRep = (z * 100) / reputation;
            print(newRep);
            barFill.fillAmount = newRep / 100;
            damageReceived = 0;
        }
        
    }
}
