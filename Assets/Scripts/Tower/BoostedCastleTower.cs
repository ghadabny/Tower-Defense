using System.Collections;
using UnityEngine;

public class BoostedCastleTower : CastleTower
{
    private bool boostActive = false;
    public int boostCost = 5;
    public GameObject freezeRockPrefab; 

    public void ActivateBoost()
    {
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
            if (targetEnemy == null) return;
            Debug.Log("Boost active: Instantiating freezeRockPrefab");
            GameObject projInstance = Instantiate(freezeRockPrefab, transform.position, Quaternion.identity);
            if (projInstance == null)
            {
                Debug.LogError("freezeRockPrefab is null!");
            }
            Projectile projScript = projInstance.GetComponent<Projectile>();
            if (projScript != null)
                projScript.SetTarget(targetEnemy);
            else
                Debug.LogError("No Projectile component found on freezeRockPrefab!");
        }
        else
        {
            base.Attack();
        }

    }
}
