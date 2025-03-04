using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public int hitCount = 0;

    public void RecordHit()
    {
        hitCount++;
    }
}
