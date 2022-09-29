using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBC2 : EnemyController
{
    protected override void SetDamageMult()
    {
        damageMult = new float[3];
        damageMult[0] = 5;
        damageMult[1] = 1;
        damageMult[2] = 0.25f;
    }
}
