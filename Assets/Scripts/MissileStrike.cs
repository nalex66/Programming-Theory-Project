using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileStrike : MonoBehaviour
{   
    // spawn missile headed to ground, ground target indicator, strike fx, collider with trigger, particle effects
    public GameObject missile;
    // public GameObject strikeIndicator; not needed now--not handling object, it exists as a child of Missile Strike
    public ParticleSystem explosion;
    public ParticleSystem damageEffect; // need to select based on damage type

    private GameManager gameManager;
    private Vector3 strikePosition;
    private float missileSpeed = 50;
    private float strikeDuration = 10;
    private float counter;
    

    
    

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        strikePosition = gameManager.targetPosition.transform.position;
        this.GetComponent<SphereCollider>().enabled = false; // will turn on collider when missile strikes
        counter = 0;
        

        // need to assign strike indicator color based on damage type
        // need to assign damage effect based on damage type
        
        // missile moves downward, is destroyed on impact -- explosion fx
        // strikeIndicator has collider with trigger to activate damage on enemies,
        

    }

    // Update is called once per frame
    void Update()
    {
        if (missile != null)
        {
            missile.transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
            if (missile.transform.position.y < 0)
            {
                Destroy(missile);
                Instantiate(explosion, strikePosition, explosion.transform.rotation);
                // instantiate particle effect based on damage type
                Instantiate(damageEffect, strikePosition, explosion.transform.rotation);
                this.GetComponent<SphereCollider>().enabled = true;
            }
        }

        counter += Time.deltaTime;
        if (counter > strikeDuration)
        {
            Destroy(gameObject); // destroy the whole missile strike after duration
        }
    }
}
