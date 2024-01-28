using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarScript : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Harlequin harlequin;
    private int reputation;

    private void Awake()
    {
        harlequin = GetComponentInParent<Harlequin>();
    }

    private void Start()
    {
        if (harlequin != null)
        {
            reputation = harlequin.GetReputation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (damageReceived > 0){
            int z = reputation - damageReceived;
            float newRep = (z * 100) / reputation;
            barFill.fillAmount = newRep / 100;
            damageReceived = 0;
        }
        
    }
}
