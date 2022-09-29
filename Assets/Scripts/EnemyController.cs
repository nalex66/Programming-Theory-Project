using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // private float speed = 1.6f;
    public float health = 10;
    private float angleOffset;
    private Vector3 direction;
    private float counter;
    private bool isDamageActive = false;
    private Animator enemyAnim;
    private ParticleSystem damageType;

    // virtual static floats for fire, ice, and electricity damage multipliers

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyAnim = GetComponent<Animator>();
        // turn towards player endzone
        direction = (gameManager.EndZone - transform.position).normalized;
        angleOffset = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        transform.Rotate(Vector3.up * angleOffset);

    }

    // Update is called once per frame
    void Update()
    {
        if (isDamageActive)
        {
            health -= 2 * Time.deltaTime; // other.damage * damagemultiplier
            counter += Time.deltaTime;
            if (counter > 10)               // var damageDuration based on enemy type, damage type?
            {
                isDamageActive = false;     // damageDuration should influence duration of particleFX?              
            }
        }

       /* if (health > 0) // trying with animation driving motion rather than translate
        {
            // if alive, move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }*/
        
        if (health < 0 && !enemyAnim.GetBool("Death_b"))
        {
            //fall over and die, start timer to destroy body
            enemyAnim.SetBool("Death_b", true);
            enemyAnim.SetInteger("DeathType_int", 2);
            StartCoroutine(DeathDelay());
        }
        

        if (transform.position.z < gameManager.EndZone.z)
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
        counter = 0; // entering new damage zone will replace current one-- one per damage type?
        isDamageActive = true; // might need way to track multiple instances of damage active

        // get damage type from other.damageIndex
        damageType = other.GetComponent<MissileStrike>().damageType;

        ParticleSystem damage = Instantiate(damageType, transform.position + Vector3.up*1f, damageType.transform.rotation) as ParticleSystem;
        damage.transform.SetParent(gameObject.transform); // activate particle effect for damage type, attach to enemy
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
