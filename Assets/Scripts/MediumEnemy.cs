using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediumEnemy : Enemy
{
    void Start()
    {
        healthPoints = 2000;
        rewardAmt = 20;
        speed = 20f;
    }
}
