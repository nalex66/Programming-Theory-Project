using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBC1 : EnemyController // INHERITENCE - Most enemy behaviour comes from the EnemyController parent class
{
    protected override void SetDamageMult() // POLYMORPHISM - default damage multipliers are overridden by ones specific to this child class
    {
        damageMult = new float[3];
        damageMult[0] = 1;
        damageMult[1] = 5;
        damageMult[2] = 0.25f;
    }

}
