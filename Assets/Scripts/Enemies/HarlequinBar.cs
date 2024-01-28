using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarlequinBar : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Harlequin harlequin;
    private int reputation;

    private float newRep;
    private float repu;

    public float GetReputation() {
        print("HARLEQUIN  health in number: " + repu + "health in number: " + newRep);
        return repu;
    }
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

    public void DoDamage(int newDamage){
        damageReceived = newDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageReceived > 0){
            int z = reputation - damageReceived;
            newRep = (z * 100) / reputation;
            harlequin.SetReputation(z);
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