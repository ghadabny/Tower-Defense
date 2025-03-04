using System.Collections;
using UnityEngine;

public class BoostedKnightPost : KnightPost
{
    private bool boostActive = false;
    public int boostCost = 5;
    public GameObject bombFireballPrefab; 

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

            GameObject projInstance = Instantiate(bombFireballPrefab, transform.position, Quaternion.identity);
            Projectile projScript = projInstance.GetComponent<Projectile>();
            if (projScript != null)
                projScript.SetTarget(targetEnemy);
        }
        else
        {
            base.Attack();
        }
    }
}
