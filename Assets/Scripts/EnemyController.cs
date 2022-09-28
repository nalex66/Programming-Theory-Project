using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 1.6f;
    public float health = 10;
    private float angleOffset;
    private Vector3 direction;
    // virtual static floats for fire, ice, and electricity damage multipliers
    
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // turn towards player endzone
        direction = (gameManager.EndZone - transform.position).normalized;
        angleOffset = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        transform.Rotate(Vector3.up * angleOffset);

    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            // if alive, move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (health < 0)
        {
            //fall over and die, start coroutine to destroy a few seconds later
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

    // check for collision with missile strike zone
    // we can do this on each enemy, or on each strike zone
    // might be easier to apply damage over time from here?
    // but more script calls with lots of enemies on the map?
     
    private void OnTriggerEnter(Collider other) 
    {
        // health -= other.damage * damagemultiplier
        // particle effect from damage source?
    }
}
