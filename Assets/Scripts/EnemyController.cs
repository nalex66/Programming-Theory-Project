using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected float[] damageMult;
    private float health = 10;
    private float angleOffset;
    private Vector3 direction;
    private float counter;
    private bool isDamageActive = false;
    private Animator enemyAnim;
    private ParticleSystem damageType;
    private int damageIndex;
    private float baseDamage = 2.0f;
    private float damageDuration = 10.0f;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyAnim = GetComponent<Animator>();

        // turn enemy towards player endzone
        direction = (gameManager.endZone - transform.position);
        angleOffset = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        transform.Rotate(Vector3.up * angleOffset);
        
        // call method to set the enemy's damage multipliers for each damage type
        SetDamageMult();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamageActive)
        {
            TakeDamage();                   // ABSTRACTION - calculations are moved to a separate method
            counter += Time.deltaTime;
            if (counter > damageDuration)
            {
                isDamageActive = false;             
            }
        }

               
        if (health < 0 && !enemyAnim.GetBool("Death_b"))
        {
            //fall over and die, start timer to destroy body
            enemyAnim.SetBool("Death_b", true);
            enemyAnim.SetInteger("DeathType_int", 2);
            StartCoroutine(DeathDelay());
        }
        

        if (transform.position.z < gameManager.endZone.z)
        {
            gameManager.GameOver();
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

         
    private void OnTriggerEnter(Collider other) 
    {
        // entering damage zone starts the counter for damage over time
        counter = 0; 
        isDamageActive = true;

        // get damage index and damage type fx from the missile strike object
        damageType = other.GetComponent<MissileStrike>().damageType;
        damageIndex = other.GetComponent<MissileStrike>().damageIndex;

        // apply damageType fx to enemy
        ParticleSystem damage = Instantiate(damageType, transform.position + Vector3.up*1.0f, damageType.transform.rotation) as ParticleSystem;
        damage.transform.SetParent(gameObject.transform); // activate particle effect for damage type, attach to enemy
    }

    protected virtual void SetDamageMult()
    {
        // this sets default damage multipliers. Child-classes can override this method to set their own values.
        damageMult = new float[3];
        damageMult[0] = 1;
        damageMult[1] = 1;
        damageMult[2] = 1;
    }

    private void TakeDamage()
    {
        // take damage over time
        health -= baseDamage * damageMult[damageIndex] * Time.deltaTime;        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(4); // let body lie on the ground for a moment before destroying
        Destroy(gameObject);
    }
}
