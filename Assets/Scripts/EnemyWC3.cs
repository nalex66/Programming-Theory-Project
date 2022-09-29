using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWC3 : EnemyController
{
    protected override void SetDamageMult()
    {
        damageMult = new float[3];
        damageMult[0] = 5;
        damageMult[1] = 0.25f;
        damageMult[2] = 1;
    }
}
