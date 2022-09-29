using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBC3 : EnemyController
{
    protected override void SetDamageMult()
    {
        damageMult = new float[3];
        damageMult[0] = 0.25f;
        damageMult[1] = 5;
        damageMult[2] = 1;
    }
}
