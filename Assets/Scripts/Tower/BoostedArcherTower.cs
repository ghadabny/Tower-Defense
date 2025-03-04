using System.Collections;
using UnityEngine;

public class BoostedArcherTower : ArcherTower
{
    private bool boostActive = false;
    public int boostCost = 3; 

    
    public void ActivateBoost()
    {
        Debug.Log("BoostedArcherTower active: " + gameObject.activeInHierarchy);
        if (EconomyManager.Instance.TotalMoney >= boostCost)
        {
            EconomyManager.Instance.SubtractMoney(boostCost);
            StartCoroutine(BoostRoutine());
        }
        else
        {
            Debug.Log("Pas assez d'argent pour le boost !");
        }
    }

    private IEnumerator BoostRoutine()
    {
        boostActive = true;
        yield return new WaitForSeconds(5f);
        boostActive = false;
    }

    protected override void Attack()
    {
        if (boostActive)
        {
            // Tirer deux projectiles en rafale
            base.Attack();
            base.Attack();
        }
        else
        {
            base.Attack();
        }
    }
}
