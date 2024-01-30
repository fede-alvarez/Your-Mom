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
        VisualFX(newRep / 100);
        damageReceived = 0;
    }

    private void VisualFX(float amount)
    {
        barFill.DOFillAmount(amount, 0.3f);
        barFill.transform.parent.DOShakeScale(0.2f, .8f, 6);
    }

    public float GetReputation()
    {
        //print("HARLEQUIN  health in number: " + repu + "health in number: " + newRep);
        return repu;
    }
}