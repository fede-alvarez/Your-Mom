using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HarlequinBar : MonoBehaviour
{

    public int damageReceived;
    public Image barFill;
    public Harlequin harlequin;
    private int reputation;
    private int maxRep;
    private float newRep;
    private float repu;

    public float GetReputation()
    {
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
            maxRep = reputation;
        }
    }

    public void DoDamage(int newDamage)
    {
        damageReceived = newDamage;

        if (damageReceived <= 0) return;

        reputation = harlequin.GetReputation();
        int z = reputation - damageReceived;
        newRep = (z * 100) / maxRep;
        harlequin.SetReputation(z);
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
    }*/
}