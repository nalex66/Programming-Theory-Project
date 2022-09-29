using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWC2 : EnemyController
{
    protected override void SetDamageMult()
    {
        damageMult = new float[3];
        damageMult[0] = 1;
        damageMult[1] = 0.25f;
        damageMult[2] = 5;
    }
}
