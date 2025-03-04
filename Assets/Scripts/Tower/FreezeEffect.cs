using UnityEngine;
using System.Collections;

public class FreezeEffect : MonoBehaviour
{
    public void ApplyFreeze(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.enabled = false;
            yield return new WaitForSeconds(duration);
            enemy.enabled = true;
        }
        Destroy(this);
    }
}
