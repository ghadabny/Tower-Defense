using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeakEnemy : Enemy
{
    void Start()
    {
        healthPoints = 50;
        rewardAmt = 10;
        speed = 1f;
    }
}
