using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongEnemy : Enemy
{
    void Start()
    {
        healthPoints = 4000;
        rewardAmt = 50;
        speed = 50f;
    }
}