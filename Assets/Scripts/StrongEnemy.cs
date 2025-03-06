using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongEnemy : Enemy
{
    void Start()
    {
        healthPoints = 200;
        rewardAmt = 50;
        speed = 3f;
    }
}